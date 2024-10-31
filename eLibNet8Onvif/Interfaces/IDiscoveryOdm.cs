﻿using System.Threading.Channels;
using odm.core;

namespace eLibNet8Onvif.Interfaces;

/// <summary>
///     Интерфейс класса, предоставляющего функциональность для асинхронного поиска Onvif устройств с использованием библиотеки <see cref="odm.core" />.
/// </summary>
public interface IDiscoveryOdm
{
    /// <summary>
    ///     Возвращает значение, указывающее, запущен ли процесс поиска.
    /// </summary>
    public bool IsStarted { get; }

    /// <summary>
    ///     Асинхронно запускает процесс поиска Onvif устройств.
    /// </summary>
    /// <param name="timeOut">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронный перечислитель найденных устройств.</returns>
    /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
    public IAsyncEnumerable<IDiscoveredCamera> StartAsync(int timeOut, CancellationToken cancellationToken);

    /// <summary>
    ///     Асинхронно запускает процесс поиска Onvif устройств с использованием указанного ChannelWriter.
    /// </summary>
    /// <param name="channelWriter">ChannelWriter для записи найденных устройств.</param>
    /// <param name="timeOut">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    /// <exception cref="Exception">Выбрасывается, если поиск уже запущен.</exception>
    public Task StartAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeOut, CancellationToken cancellationToken);
}