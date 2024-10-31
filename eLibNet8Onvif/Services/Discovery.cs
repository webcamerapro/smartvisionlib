using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Xml;
using System.Xml.Serialization;
using eLibNet8Core.Extensions;
using eLibNet8Onvif.Constants;
using eLibNet8Onvif.Exceptions;
using eLibNet8Onvif.Interfaces;
using eLibNet8Onvif.Models;

namespace eLibNet8Onvif.Services;

/// <summary>
///     Класс, предоставляющий функционал для обнаружения Onvif устройств.
/// </summary>
public class Discovery : IDiscovery
{
    private readonly DiscoveryClientFactory _discoveryClientFactory = new();

    /// <summary>
    ///     Запускает асинхронное обнаружение устройств.
    /// </summary>
    /// <param name="timeout">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронный перечислитель обнаруженных камер.</returns>
    public IAsyncEnumerable<IDiscoveredCamera> StartAsync(int timeout, CancellationToken cancellationToken = default)
    {
        var channel = Channel.CreateUnbounded<IDiscoveredCamera>();
        _ = DiscoverFromAllClients(channel.Writer, timeout, cancellationToken);
        return channel.Reader.ReadAllAsync(cancellationToken);
    }

    /// <summary>
    ///     Запускает асинхронное обнаружение устройств с использованием указанного канала записи.
    /// </summary>
    /// <param name="channelWriter">Канал записи для обнаруженных камер.</param>
    /// <param name="timeout">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public Task StartAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeout, CancellationToken cancellationToken = default) => DiscoverFromAllClients(channelWriter, timeout, cancellationToken);

    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    private async Task DiscoverFromAllClients(ChannelWriter<IDiscoveredCamera> channelWriter, int timeout, CancellationToken cancellationToken)
    {
        using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
        using var cts        = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);
        try
        {
            var clients = _discoveryClientFactory.CreateDiscoveryClients().ToArray();
            if (clients.Length == 0) throw new DiscoveryException("Missing valid NetworkInterfaces, UdpClients could not be created");
            var discoveries = clients.Select(client => DiscoverFromClient(channelWriter, client, new(), cts.Token));
            await Task.WhenAll(discoveries);
        } catch (Exception e) when (e is OperationCanceledException or TaskCanceledException && timeoutCts.IsCancellationRequested)
        {
            //ignored
        } catch (Exception e)
        {
            channelWriter.TryComplete(e);
            throw;
        } finally
        {
            channelWriter.TryComplete();
        }
    }

    private static async Task DiscoverFromClient(ChannelWriter<IDiscoveredCamera> channelWriter, IDiscoveryClient client, ConcurrentDictionary<string, bool> discoveredDevicesAddresses, CancellationToken cancellationToken)
    {
        try
        {
            var messageId = Guid.NewGuid();
            await Task.WhenAll(SendProbes(client, messageId, cancellationToken), ReceiveAnswers(channelWriter, client, discoveredDevicesAddresses, messageId, cancellationToken));
        } finally
        {
            client.Close();
        }
    }

    private static async Task SendProbes(IDiscoveryClient client, Guid messageId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (messageId == Guid.Empty)
            throw new ArgumentException("messageId не должен быть пустым");
        var multicastEndpoint = new IPEndPoint(IPAddress.Parse(FieldConstants.MulticastAddress), FieldConstants.MulticastPort);
        var datagram          = Encoding.ASCII.GetBytes(string.Format(FieldConstants.ProbeMessage, messageId.ToString()));
        while (!cancellationToken.IsCancellationRequested)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await client.SendAsync(datagram, multicastEndpoint, cancellationToken);
            await Task.Delay(500, cancellationToken);
        }
    }

    private static IDiscoveredCamera? ProcessResponse(UdpReceiveResult response, Guid messageId)
    {
        using var textReader  = new StringReader(Encoding.UTF8.GetString(response.Buffer));
        using var xmlReader   = XmlReader.Create(textReader, new());
        var       xmlResponse = new XmlSerializer(typeof(XmlProbeResponse)).Deserialize(xmlReader) as XmlProbeResponse;
        if ((xmlResponse?.Header.RelatesTo.Contains(messageId.ToString()) ?? false) && xmlResponse.Body.ProbeMatches.Length != 0 && !xmlResponse.Body.ProbeMatches[0].Scopes.IsEmpty())
            return CreateDevice(xmlResponse.Body.ProbeMatches[0], response.RemoteEndPoint);
        return null;
    }

    private static IDiscoveredCamera CreateDevice(ProbeMatch probeMatch, IPEndPoint remoteEndpoint)
    {
        var scopesArray  = probeMatch.Scopes.Split();
        var mfrQuery     = scopesArray.Where(scope => scope.Contains("mfr/") || scope.Contains("manufacturer/")).ToArray();
        var manufacturer = mfrQuery.Length > 0 ? Uri.UnescapeDataString(RegexConstants.OnvifUriRegex().Match(mfrQuery[0]).Groups[6].Value) : string.Empty;
        if (!manufacturer.IsEmpty())
            return new DiscoveredCamera(remoteEndpoint.Address, manufacturer, Uri.UnescapeDataString(RegexConstants.OnvifHardwareRegex().Match(probeMatch.Scopes).Value), probeMatch.Scopes.Split().Select(str => str.Trim()),
                probeMatch.Types.Split().Select(str => str.Trim()),
                probeMatch.XAddrs.Split().Select(str => str.Trim()));
        var nameQuery = scopesArray.Where(scope => scope.Contains("name/")).ToArray();
        manufacturer = nameQuery.Length > 0 ? Uri.UnescapeDataString(RegexConstants.OnvifUriRegex().Match(nameQuery[0]).Groups[6].Value) : string.Empty;
        if (manufacturer.Contains(' '))
            manufacturer = manufacturer.Split()[0];
        return new DiscoveredCamera(remoteEndpoint.Address, manufacturer, Uri.UnescapeDataString(RegexConstants.OnvifHardwareRegex().Match(probeMatch.Scopes).Value), probeMatch.Scopes.Split().Select(str => str.Trim()),
            probeMatch.Types.Split().Select(str => str.Trim()),
            probeMatch.XAddrs.Split().Select(str => str.Trim()));
    }

    private static async Task ReceiveAnswers(ChannelWriter<IDiscoveredCamera> channelWriter, IDiscoveryClient client, ConcurrentDictionary<string, bool> discoveredDevicesAddresses, Guid messageId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await foreach (var response in client.ReceiveResultsAsync(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var discoveredDevice = ProcessResponse(response, messageId);
            if (discoveredDevice is null || discoveredDevice.XAddresses.All(discoveredDevicesAddresses.ContainsKey))
                continue;
            foreach (var xAddress in discoveredDevice.XAddresses)
                discoveredDevicesAddresses.TryAdd(xAddress, true);
            await channelWriter.WriteAsync(discoveredDevice, cancellationToken);
        }
    }
}