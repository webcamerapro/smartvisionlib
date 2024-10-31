using System;
using System.Threading;
using System.Threading.Tasks;
using eLibNet4Core.Contexts;
using JetBrains.Annotations;

namespace eLibNet4Core.Helpers
{
    /// <summary>
    ///     Класс, содержащий методы для выполнения асинхронных задач и возвращения управления в текущий поток.
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        ///     Выполняет асинхронную задачу и возвращает управление в текущий поток.
        /// </summary>
        /// <param name="func">Функция, представляющая асинхронную задачу.</param>
        public static void RunAsyncAndReturnToCurrentThread([InstantHandle(RequireAwait = true)] Func<Task> func)
        {
            var oldSyncContext = SynchronizationContext.Current;
            try
            {
                var newSyncContext = new SingleThreadSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(newSyncContext);
                var task = func();
                task.ContinueWith(delegate { newSyncContext.Complete(); }, TaskScheduler.Default);
                newSyncContext.Run();
                Task.Run(() => task).GetAwaiter().GetResult();
            } finally
            {
                SynchronizationContext.SetSynchronizationContext(oldSyncContext);
            }
        }

        /// <summary>
        ///     Выполняет асинхронную задачу и возвращает результат в текущий поток.
        /// </summary>
        /// <typeparam name="TResult">Тип результата асинхронной задачи.</typeparam>
        /// <param name="func">Функция, представляющая асинхронную задачу.</param>
        /// <returns>Результат выполнения асинхронной задачи.</returns>
        public static TResult RunAsyncAndReturnToCurrentThread<TResult>([InstantHandle(RequireAwait = true)] Func<Task<TResult>> func)
        {
            var oldSyncContext = SynchronizationContext.Current;
            try
            {
                var newSyncContext = new SingleThreadSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(newSyncContext);
                var task = func();
                task.ContinueWith(delegate { newSyncContext.Complete(); }, TaskScheduler.Default);
                newSyncContext.Run();
                return Task.Run(() => task).GetAwaiter().GetResult();
            } finally
            {
                SynchronizationContext.SetSynchronizationContext(oldSyncContext);
            }
        }
    }
}