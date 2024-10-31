using System.Collections.Concurrent;

namespace eLibNet8Core.Contexts;

/// <summary>
///     Класс для простой синхронизации в один поток.
/// </summary>
public sealed class SingleThreadSynchronizationContext : SynchronizationContext
{
    /// <summary>
    ///     Коллекция для хранения пар обратных вызовов и их состояний.
    /// </summary>
    private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object?>> _queue = new();

    /// <summary>
    ///     Добавляет обратный вызов в очередь для выполнения в синхронизированном потоке.
    /// </summary>
    /// <param name="d">Обратный вызов для выполнения.</param>
    /// <param name="state">Состояние, передаваемое в обратный вызов.</param>
    public override void Post(SendOrPostCallback d, object? state) { _queue.Add(new(d, state)); }

    /// <summary>
    ///     Запускает выполнение всех обратных вызовов в очереди.
    /// </summary>
    public void Run()
    {
        while (_queue.TryTake(out var item, Timeout.Infinite))
            item.Key(item.Value);
    }

    /// <summary>
    ///     Завершает добавление новых элементов в очередь.
    /// </summary>
    public void Complete() { _queue.CompleteAdding(); }
}