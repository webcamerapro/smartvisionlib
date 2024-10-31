using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using eLibNet8Core.Extensions;
using eLibNet8Core.Helpers;
using eLibNet8Onvif.Extensions;
using eLibNet8Onvif.Interfaces;
using eLibNet8Onvif.Models;
using odm.core;
using utils;

namespace eLibNet8Onvif.Services;

/// <summary>
///     Класс, предоставляющий функциональность для асинхронного поиска Onvif устройств с использованием библиотеки <see cref="odm.core" />.
/// </summary>
public class DiscoveryOdm : IDiscoveryOdm
{
    private readonly INvtManager _manager = new NvtManager();
    private ChannelWriter<IDiscoveredCamera>? _channelWriter;
    private CancellationTokenSource? _ctsLinked;
    private CancellationTokenSource? _ctsTimeOut;

    /// <summary>
    ///     Возвращает значение, указывающее, запущен ли процесс поиска.
    /// </summary>
    public bool IsStarted { get; private set; }

    /// <summary>
    ///     Асинхронно запускает процесс поиска Onvif устройств.
    /// </summary>
    /// <param name="timeOut">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронный перечислитель найденных устройств.</returns>
    /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
    public IAsyncEnumerable<IDiscoveredCamera> StartAsync(int timeOut, CancellationToken cancellationToken = default)
    {
        if (IsStarted)
            throw new("Поиск уже запущен.");
        try
        {
            IsStarted = true;
            DisposeHelper.DisposeAndSet(ref _ctsTimeOut, new(TimeSpan.FromSeconds(timeOut)));
            DisposeHelper.DisposeAndSet(ref _ctsLinked, CancellationTokenSource.CreateLinkedTokenSource(_ctsTimeOut.Token, cancellationToken));
            var channel = Channel.CreateUnbounded<IDiscoveredCamera>();
            _ = DiscoveryAsync(channel.Writer, timeOut, _ctsLinked.Token);
            return channel.Reader.ReadAllAsync(cancellationToken);
        } finally
        {
            IsStarted = false;
        }
    }

    /// <summary>
    ///     Асинхронно запускает процесс поиска Onvif устройств с использованием указанного ChannelWriter.
    /// </summary>
    /// <param name="channelWriter">ChannelWriter для записи найденных устройств.</param>
    /// <param name="timeOut">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
    public async Task StartAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeOut, CancellationToken cancellationToken = default)
    {
        if (IsStarted)
            throw new("Поиск уже запущен.");
        try
        {
            IsStarted = true;
            DisposeHelper.DisposeAndSet(ref _ctsTimeOut, new(TimeSpan.FromSeconds(timeOut)));
            DisposeHelper.DisposeAndSet(ref _ctsLinked, CancellationTokenSource.CreateLinkedTokenSource(_ctsTimeOut.Token, cancellationToken));
            await DiscoveryAsync(channelWriter, timeOut, _ctsLinked.Token).ConfigureAwait(false);
        } finally
        {
            IsStarted = false;
        }
    }

    private async Task DiscoveryAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeOut, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _channelWriter = channelWriter;
        await Task.Run(() =>
        {
            _manager.Observe().TakeUntil(cancellationToken).OnCompleted(CompletedAction).OnError(ErrorAction).Subscribe(SubscribeAction);
            _manager.Discover(TimeSpan.FromSeconds(timeOut));
        }, cancellationToken).ConfigureAwait(false);
    }

    private void CompletedAction()
    {
        _channelWriter?.TryComplete();
        DisposeHelper.DisposeAndDefault(ref _ctsTimeOut);
        DisposeHelper.DisposeAndDefault(ref _ctsLinked);
    }

    private void SubscribeAction(INvtNode nvtNode)
    {
        var nvtIdentity = nvtNode.identity;
        if (nvtIdentity.uris.Length == 0)
            return;
        var ipAddress = nvtIdentity.uris.Select(uri => Dns.GetHostAddresses(uri.Host)[0]).FirstOrDefault(uriIpAddress => uriIpAddress.AddressFamily == AddressFamily.InterNetwork);
        if (ipAddress is null)
            return;
        _channelWriter?.TryWrite(new DiscoveredCamera(ipAddress,
            nvtIdentity.scopes.FirstOrDefault(scope => scope.AbsolutePath.Contains("manufacturer/") || scope.AbsolutePath.Contains("mfr/") ||
                                                       scope.AbsolutePath.Contains("name/"))?.GetRightAfterSegmentsPriority("manufacturer/", "mfr/", "name/") ?? "Unknown",
            nvtIdentity.scopes.FirstOrDefault(scope => scope.AbsolutePath.Contains("hardware/"))?.GetRightAfterSegment("hardware/") ?? "Unknown",
            nvtIdentity.scopes,
            nvtIdentity.uris));
    }

    private void ErrorAction(Exception e)
    {
        if (e is OperationCanceledException)
            _channelWriter?.TryComplete();
        else
            _channelWriter?.TryComplete(e);
        DisposeHelper.DisposeAndDefault(ref _ctsTimeOut);
        DisposeHelper.DisposeAndDefault(ref _ctsLinked);
    }
}