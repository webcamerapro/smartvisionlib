using System.Net;
using System.Net.Sockets;
using eLibNet8Core.Interfaces;

namespace eLibNet8Onvif.Interfaces;

internal interface IDiscoveryClient : IDisposablePattern
{
    public Task<int> SendAsync(byte[] datagram, IPEndPoint endPoint, CancellationToken cancellationToken);

    public IAsyncEnumerable<UdpReceiveResult> ReceiveResultsAsync(CancellationToken cancellationToken);

    public void Close();
}