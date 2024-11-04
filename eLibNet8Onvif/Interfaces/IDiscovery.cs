using System.Threading.Channels;

namespace eLibNet8Onvif.Interfaces;

/// <summary>
///     Интерфейс класса предоставляющего функционал для обнаружения Onvif устройств.
/// </summary>
public interface IDiscovery
{
    /// <summary>
    ///     Возвращает значение, указывающее, запущен ли процесс поиска.
    /// </summary>
    public bool IsStarted { get; }

    /// <summary>
    ///     Запускает асинхронное обнаружение устройств.
    /// </summary>
    /// <param name="timeout">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронный перечислитель обнаруженных камер.</returns>
    public IAsyncEnumerable<IDiscoveredCamera> StartAsync(int timeout, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Запускает асинхронное обнаружение устройств с использованием указанного канала записи.
    /// </summary>
    /// <param name="channelWriter">Канал записи для обнаруженных камер.</param>
    /// <param name="timeout">Таймаут в секундах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public Task StartAsync(ChannelWriter<IDiscoveredCamera> channelWriter, int timeout, CancellationToken cancellationToken = default);
}