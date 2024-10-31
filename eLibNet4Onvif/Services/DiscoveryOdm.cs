using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using eLibNet4Core.Extensions;
using eLibNet4Core.Helpers;
using eLibNet4Onvif.Constants;
using eLibNet4Onvif.Extensions;
using eLibNet4Onvif.Interfaces;
using eLibNet4Onvif.Models;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using odm.core;
using utils;

namespace eLibNet4Onvif.Services
{
    /// <summary>
    ///     Класс, предоставляющий функциональность для асинхронного поиска Onvif устройств с использованием библиотеки <see cref="odm.core" />.
    /// </summary>
    public class DiscoveryOdm : IDiscoveryOdm
    {
        private readonly INvtManager _manager = new NvtManager();
        private ChannelWriter<INvtNode> _channelWriter;
        private CancellationTokenSource _ctsLinked;
        private CancellationTokenSource _ctsTimeOut;

        /// <summary>
        ///     Возвращает значение, указывающее, запущен ли процесс поиска.
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        ///     Запускает процесс поиска Onvif устройств.
        /// </summary>
        /// <param name="timeOut">Таймаут в секундах.</param>
        /// <param name="discoveredAction">Действие, выполняемое при обнаружении устройства.</param>
        /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
        public void Start(int timeOut, [NotNull] Action<INvtNode> discoveredAction)
        {
            if (IsStarted)
                throw new Exception("Поиск уже запущен.");
            try
            {
                IsStarted = true;
                _manager.Observe().Subscribe(discoveredAction);
                _manager.Discover(TimeSpan.FromSeconds(timeOut));
            } finally
            {
                IsStarted = false;
            }
        }

        /// <summary>
        ///     Асинхронно запускает процесс поиска Onvif устройств.
        /// </summary>
        /// <param name="timeOut">Таймаут в секундах.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Асинхронный перечислитель найденных устройств.</returns>
        /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
        public IAsyncEnumerable<INvtNode> StartAsync(int timeOut, CancellationToken cancellationToken = default)
        {
            if (IsStarted)
                throw new Exception("Поиск уже запущен.");
            try
            {
                IsStarted = true;
                DisposeHelper.DisposeAndSet(ref _ctsTimeOut, new CancellationTokenSource(TimeSpan.FromSeconds(timeOut)));
                DisposeHelper.DisposeAndSet(ref _ctsLinked, CancellationTokenSource.CreateLinkedTokenSource(_ctsTimeOut.Token, cancellationToken));
                var channel = Channel.CreateUnbounded<INvtNode>();
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
        public async Task StartAsync(ChannelWriter<INvtNode> channelWriter, int timeOut, CancellationToken cancellationToken = default)
        {
            if (IsStarted)
                throw new Exception("Поиск уже запущен.");
            try
            {
                IsStarted = true;
                DisposeHelper.DisposeAndSet(ref _ctsTimeOut, new CancellationTokenSource(TimeSpan.FromSeconds(timeOut)));
                DisposeHelper.DisposeAndSet(ref _ctsLinked, CancellationTokenSource.CreateLinkedTokenSource(_ctsTimeOut.Token, cancellationToken));
                await DiscoveryAsync(channelWriter, timeOut, _ctsLinked.Token).ConfigureAwait(false);
            } finally
            {
                IsStarted = false;
            }
        }

        private async Task DiscoveryAsync(ChannelWriter<INvtNode> channelWriter, int timeOut, CancellationToken cancellationToken)
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
            var nvtIdentity        = nvtNode.identity;
            if (nvtIdentity.uris.Length == 0) 
                return;
            foreach (var uri in nvtIdentity.uris)
            {

                var orgIP= uri.AbsoluteUri.ToString();

                JObject js = new JObject();
                js["hw"]     = hwName;
                js["manuf"]  = hwManuf;
                js["url"]    = orgIP;
                js["urn"]    = orgKey;
                js["source"] = Assembly.GetExecutingAssembly().GetName().Name;
                js["stamp"]  = System.DateTime.Now.ToUniversalTime().ToString();
                
                {
                    Uri myUri = new Uri(orgIP);
                    var ip    = ;
                    js["ip"] = ip.ToString();
                }
                jsarr.Add(js);
                log.Log(js.ToString());
            }
            
            
            
            
            
            
            var mfrQuery     = nvtIdentity.scopes.Where(scope => scope.AbsolutePath.Contains("mfr/") || scope.AbsolutePath.Contains("manufacturer/")).ToArray();
            var manufacturer = mfrQuery.Length > 0 ? Uri.UnescapeDataString(RegexConstants.OnvifUriRegex.Match(mfrQuery[0].AbsolutePath).Groups[6].Value) : string.Empty;
            if (!manufacturer.IsEmpty())
                return new DiscoveredCamera(remoteEndpoint.Address, manufacturer, Uri.UnescapeDataString(RegexConstants.OnvifHardwareRegex.Match(probeMatch.Scopes).Value), probeMatch.Scopes.Split().Select(str => str.Trim()),
                    probeMatch.Types.Split().Select(str => str.Trim()),
                    probeMatch.XAddrs.Split().Select(str => str.Trim()));
            var nameQuery = scopesArray.Where(scope => scope.Contains("name/")).ToArray();
            manufacturer = nameQuery.Length > 0 ? Uri.UnescapeDataString(RegexConstants.OnvifUriRegex.Match(nameQuery[0]).Groups[6].Value) : string.Empty;
            if (manufacturer.Contains(' '))
                manufacturer = manufacturer.Split()[0];
            return new DiscoveredCamera(remoteEndpoint.Address, manufacturer, Uri.UnescapeDataString(RegexConstants.OnvifHardwareRegex.Match(probeMatch.Scopes).Value), probeMatch.Scopes.Split().Select(str => str.Trim()),
                probeMatch.Types.Split().Select(str => str.Trim()),
                probeMatch.XAddrs.Split().Select(str => str.Trim()));
            _channelWriter?.TryWrite(nvtNode); 
            
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
}