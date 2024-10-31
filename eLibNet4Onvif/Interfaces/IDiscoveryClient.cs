using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using eLibNet4Core.Interfaces;

namespace eLibNet4Onvif.Interfaces
{
    internal interface IDiscoveryClient : IDisposablePattern
    {
        Task<int> SendAsync(byte[] datagram, IPEndPoint endPoint, CancellationToken cancellationToken);

        IAsyncEnumerable<UdpReceiveResult> ReceiveResultsAsync(CancellationToken cancellationToken);

        void Close();
    }
}