using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using eLibNet4Core.Extensions;
using eLibNet4Onvif.Extensions;
using eLibNet4Onvif.Interfaces;
using eLibNet4Onvif.Models;
using JetBrains.Annotations;
using odm.core;
using utils;

namespace eLibNet4Onvif.Services
{
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
        /// <returns>Асинхронный перечислитель найденных устройств.</returns>
        /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
        public IAsyncEnumerable<IDiscoveredCamera> StartAsync(int timeOut, CancellationToken cancellationToken = default)
        {
            if (IsStarted)
                throw new Exception("Поиск уже запущен.");
            try
            {
                IsStarted = true;
                var channel = Channel.CreateUnbounded<IDiscoveredCamera>();
                _ = DiscoveryAsync(channel.Writer, timeOut, cancellationToken);
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
                throw new Exception("Поиск уже запущен.");
            try
            {
                IsStarted = true;
                await DiscoveryAsync(channelWriter, timeOut, cancellationToken).ConfigureAwait(false);
            } finally
            {
                IsStarted = false;
            }
        }

        private static async Task DiscoveryAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeOut, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                using (var ctsTimeOut = new CancellationTokenSource(TimeSpan.FromSeconds(timeOut)))
                {
                    using (var ctsLinked = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeOut.Token, cancellationToken))
                    {
                        INvtManager nvtManager = new NvtManager();
                        nvtManager.Observe().TakeUntil(ctsLinked.Token).OnCompleted(() =>
                        {
                            channelWriter.TryComplete();
                        }).OnError(exception =>
                        {
                            if (exception is OperationCanceledException)
                                channelWriter.TryComplete();
                            else
                                channelWriter.TryComplete(exception);
                        }).Subscribe(nvtNode =>
                        {
                            if (TryCreateDiscoveredCamera(nvtNode, out var discoveredCamera))
                                channelWriter.TryWrite(discoveredCamera);
                        });
                        nvtManager.Discover(TimeSpan.FromSeconds(timeOut));
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }
        
        private static bool TryCreateDiscoveredCamera(INvtNode nvtNode, [CanBeNull] out IDiscoveredCamera discoveredCamera)
        {
            discoveredCamera = null;
            var nvtIdentity = nvtNode.identity;
            if (nvtIdentity.uris.Length == 0)
                return false;
            var ipAddress = nvtIdentity.uris.Select(uri => Dns.GetHostAddresses(uri.Host)[0]).FirstOrDefault(uriIpAddress => uriIpAddress.AddressFamily == AddressFamily.InterNetwork);
            if (ipAddress is null)
                return false;
            discoveredCamera = new DiscoveredCamera(ipAddress,
                nvtIdentity.scopes.FirstOrDefault(scope => scope.AbsolutePath.Contains("manufacturer/") || scope.AbsolutePath.Contains("mfr/") ||
                                                           scope.AbsolutePath.Contains("name/"))?.GetRightAfterSegmentsPriority("manufacturer/", "mfr/", "name/") ?? "Unknown",
                nvtIdentity.scopes.FirstOrDefault(scope => scope.AbsolutePath.Contains("hardware/"))?.GetRightAfterSegment("hardware/") ?? "Unknown",
                nvtIdentity.scopes,
                nvtIdentity.uris);
            return true;
        }
    }
}