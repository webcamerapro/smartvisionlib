using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using eLibNet8Core.Helpers;
using eLibNet8Core.Patterns;
using eLibNet8Onvif.Interfaces;

namespace eLibNet8Onvif.Models;

internal class DiscoveryClient(IPEndPoint localEndpoint) : DisposablePattern, IDiscoveryClient
{
    private UdpClient? _udpClient = new(localEndpoint)
    {
        EnableBroadcast = true
    };

    private UdpClient Client => _udpClient ?? throw new NullReferenceException("DiscoveryClient был освобожден");

    public async Task<int> SendAsync(byte[] datagram, IPEndPoint endPoint, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Client.SendAsync(datagram, datagram.Length, endPoint);
    }

    public async IAsyncEnumerable<UdpReceiveResult> ReceiveResultsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        while (!cancellationToken.IsCancellationRequested)
        {
            cancellationToken.ThrowIfCancellationRequested();
            UdpReceiveResult? response = null;
            try
            {
                response = await Client.ReceiveAsync(cancellationToken);
            } catch (Exception e) when (e is TaskCanceledException or OperationCanceledException)
            {
                throw;
            } catch (Exception)
            {
                //ignored
            }

            if (response.HasValue)
                yield return response.Value;
        }
    }

    public void Close() { _udpClient?.Close(); }

    protected override void DisposeManagedResources() { }

    protected override void DisposeUnmanagedResources()
    {
        Close();
        DisposeHelper.DisposeAndDefault(ref _udpClient);
    }
}