using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace eLibNet4Core.Contexts
{
    /// <summary>
    ///     Класс для простой синхронизации в один поток.
    /// </summary>
    public sealed class SingleThreadSynchronizationContext : SynchronizationContext
    {
        /// <summary>
        ///     Коллекция для хранения пар обратных вызовов и их состояний.
        /// </summary>
        private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> _queue = new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();

        /// <summary>
        ///     Добавляет обратный вызов в очередь для выполнения в синхронизированном потоке.
        /// </summary>
        /// <param name="d">Обратный вызов для выполнения.</param>
        /// <param name="state">Состояние, передаваемое в обратный вызов.</param>
        public override void Post(SendOrPostCallback d, object state) { _queue.Add(new KeyValuePair<SendOrPostCallback, object>(d, state)); }

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
}