using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Dasync.Collections;
using eLibNet4Core.Helpers;
using eLibNet4Core.Patterns;
using eLibNet4Onvif.Interfaces;
using JetBrains.Annotations;

namespace eLibNet4Onvif.Models
{
    internal class DiscoveryClient : DisposablePattern, IDiscoveryClient
    {
        [CanBeNull]
        private UdpClient _udpClient;

        public DiscoveryClient(IPEndPoint localEndpoint) => _udpClient = new UdpClient(localEndpoint)
        {
            EnableBroadcast = true
        };

        [NotNull]
        private UdpClient Client => _udpClient ?? throw new NullReferenceException("DiscoveryClient был освобожден");

        public async Task<int> SendAsync(byte[] datagram, IPEndPoint endPoint, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Client.SendAsync(datagram, datagram.Length, endPoint);
        }

        public IAsyncEnumerable<UdpReceiveResult> ReceiveResultsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return new AsyncEnumerable<UdpReceiveResult>(async yield =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    UdpReceiveResult? response = null;
                    try
                    {
                        response = await Client.ReceiveAsync();
                    } catch (Exception e) when (e is TaskCanceledException || e is OperationCanceledException)
                    {
                        throw;
                    } catch (Exception)
                    {
                        //ignored
                    }

                    if (response.HasValue)
                        await yield.ReturnAsync(response.Value);
                }
            });
        }

        public void Close() { _udpClient?.Close(); }

        protected override void DisposeManagedResources() { }

        protected override void DisposeUnmanagedResources()
        {
            Close();
            DisposeHelper.DisposeAndDefault(ref _udpClient);
        }
    }
}