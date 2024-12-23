﻿using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using eLibNet8Core.Extensions;
using eLibNet8Onvif.Exceptions;
using eLibNet8Onvif.Extensions;
using eLibNet8Onvif.Interfaces;
using eLibNet8Onvif.Models;
using odm.core;
using utils;

namespace eLibNet8Onvif.Services;

/// <summary>
///     Класс, предоставляющий функциональность для асинхронного поиска Onvif устройств с использованием библиотеки <see cref="odm.core" />.
/// </summary>
public class DiscoveryOdm : IDiscovery
{
    /// <summary>
    ///     Возвращает значение, указывающее, запущен ли процесс поиска.
    /// </summary>
    public bool IsStarted { get; private set; }

    /// <summary>
    ///     Асинхронно запускает процесс поиска Onvif устройств.
    /// </summary>
    /// <param name="timeOut">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронный перечислитель <see cref="IDiscoveredCamera" /> найденных устройств.</returns>
    /// <exception cref="DiscoveryException">Выбрасывается, если поиск уже запущен.</exception>
    public IAsyncEnumerable<IDiscoveredCamera> StartAsync(int timeOut, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var channel = Channel.CreateUnbounded<IDiscoveredCamera>();
        _ = DiscoveryAsync(channel.Writer, timeOut, cancellationToken).ConfigureAwait(false);
        return channel.Reader.ReadAllAsync(cancellationToken);
    }

    /// <summary>
    ///     Асинхронно запускает процесс поиска Onvif устройств с использованием указанного ChannelWriter.
    /// </summary>
    /// <param name="channelWriter">ChannelWriter для записи найденных устройств.</param>
    /// <param name="timeOut">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    /// <exception cref="DiscoveryException">Выбрасывается, если поиск уже запущен.</exception>
    public async Task StartAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeOut, CancellationToken cancellationToken = default) => await DiscoveryAsync(channelWriter, timeOut, cancellationToken).ConfigureAwait(false);

    private async Task DiscoveryAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeOut, CancellationToken cancellationToken) => await Task.Run(() =>
    {
        if (IsStarted)
            throw new DiscoveryException("Поиск уже запущен.");
        cancellationToken.ThrowIfCancellationRequested();
        IsStarted = true;
        var         ctsTimeOut = new CancellationTokenSource(TimeSpan.FromSeconds(timeOut));
        var         ctsLinked  = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeOut.Token, cancellationToken);
        INvtManager nvtManager = new NvtManager();
        nvtManager.Observe().TakeUntil(ctsLinked.Token).OnCompleted(() =>
        {
            channelWriter.TryComplete();
            IsStarted = false;
            ctsLinked.Dispose();
            ctsTimeOut.Dispose();
        }).OnError(e =>
        {
            switch (e)
            {
                case OperationCanceledException when ctsTimeOut.IsCancellationRequested:
                    e = new TimeoutException("Время выполнения операции истекло.");
                    channelWriter.TryComplete(e);
                    break;
                case OperationCanceledException:
                    channelWriter.TryComplete();
                    break;
                default:
                    channelWriter.TryComplete(e);
                    break;
            }

            IsStarted = false;
            ctsLinked.Dispose();
            ctsTimeOut.Dispose();
        }).Subscribe(nvtNode =>
        {
            var nvtIdentity = nvtNode.identity;
            if (nvtIdentity.uris.Length == 0)
                return;
            var ipAddress = nvtIdentity.uris.Select(uri => Dns.GetHostAddresses(uri.Host)[0]).FirstOrDefault(uriIpAddress => uriIpAddress.AddressFamily == AddressFamily.InterNetwork);
            if (ipAddress is null)
                return;
            channelWriter.TryWrite(new DiscoveredCamera(ipAddress,
                nvtIdentity.scopes.FirstOrDefault(scope => scope.AbsolutePath.Contains("manufacturer/") || scope.AbsolutePath.Contains("mfr/") ||
                                                           scope.AbsolutePath.Contains("name/"))?.GetRightAfterSegmentsPriority("manufacturer/", "mfr/", "name/") ?? "Unknown",
                nvtIdentity.scopes.FirstOrDefault(scope => scope.AbsolutePath.Contains("hardware/"))?.GetRightAfterSegment("hardware/") ?? "Unknown",
                nvtIdentity.scopes,
                nvtIdentity.uris));
        });
        nvtManager.Discover(TimeSpan.FromSeconds(timeOut));
    }, cancellationToken).ConfigureAwait(false);
}