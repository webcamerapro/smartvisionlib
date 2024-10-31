using eLibNet8Core.Extensions;
using JetBrains.Annotations;

namespace eLibNet8Core.Helpers;

/// <summary>
///     Класс, содержащий методы для выполнения действий и функций в контексте мьютекса, обеспечивая блокировку доступа к ресурсу до завершения выполнения.
/// </summary>
public static class MutexHelper
{
    /// <summary>
    ///     Выполняет действие в контексте мьютекса, блокируя доступ к ресурсу до завершения действия.
    /// </summary>
    /// <param name="action">Действие для выполнения.</param>
    /// <param name="initiallyOwned">Указывает, владеет ли вызывающий поток мьютексом при его создании.</param>
    /// <param name="name">Имя мьютекса (необязательно).</param>
    public static void MutexWaitOne([InstantHandle(RequireAwait = true)] Action action, bool initiallyOwned = false, string? name = null)
    {
        using Mutex mutex = name.IsEmpty() ? new(initiallyOwned) : new(initiallyOwned, name);
        try
        {
            mutex.WaitOne();
            action();
        } finally
        {
            mutex.ReleaseMutex();
        }
    }

    /// <summary>
    ///     Выполняет функцию в контексте мьютекса, блокируя доступ к ресурсу до завершения функции.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения функции.</typeparam>
    /// <param name="func">Функция для выполнения.</param>
    /// <param name="initiallyOwned">Указывает, владеет ли вызывающий поток мьютексом при его создании.</param>
    /// <param name="name">Имя мьютекса (необязательно).</param>
    /// <returns>Результат выполнения функции.</returns>
    public static T MutexWaitOne<T>([InstantHandle(RequireAwait = true)] Func<T> func, bool initiallyOwned = false, string? name = null)
    {
        using Mutex mutex = name.IsEmpty() ? new(initiallyOwned) : new(initiallyOwned, name);
        try
        {
            mutex.WaitOne();
            return func();
        } finally
        {
            mutex.ReleaseMutex();
        }
    }

    /// <summary>
    ///     Асинхронно выполняет функцию в контексте мьютекса, блокируя доступ к ресурсу до завершения функции.
    /// </summary>
    /// <param name="func">Асинхронная функция для выполнения.</param>
    /// <param name="initiallyOwned">Указывает, владеет ли вызывающий поток мьютексом при его создании.</param>
    /// <param name="name">Имя мьютекса (необязательно).</param>
    public static void MutexWaitOneAsync([InstantHandle(RequireAwait = true)] Func<Task> func, bool initiallyOwned = false, string? name = null)
    {
        using Mutex mutex = name.IsEmpty() ? new(initiallyOwned) : new(initiallyOwned, name);
        try
        {
            mutex.WaitOne();
            AsyncHelper.RunAsyncAndReturnToCurrentThread(func);
        } finally
        {
            mutex.ReleaseMutex();
        }
    }

    /// <summary>
    ///     Асинхронно выполняет функцию в контексте мьютекса, блокируя доступ к ресурсу до завершения функции.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения функции.</typeparam>
    /// <param name="func">Асинхронная функция для выполнения.</param>
    /// <param name="initiallyOwned">Указывает, владеет ли вызывающий поток мьютексом при его создании.</param>
    /// <param name="name">Имя мьютекса (необязательно).</param>
    /// <returns>Результат выполнения функции.</returns>
    public static T MutexWaitOneAsync<T>([InstantHandle(RequireAwait = true)] Func<Task<T>> func, bool initiallyOwned = false, string? name = null)
    {
        using Mutex mutex = name.IsEmpty() ? new(initiallyOwned) : new(initiallyOwned, name);
        try
        {
            mutex.WaitOne();
            return AsyncHelper.RunAsyncAndReturnToCurrentThread(func);
        } finally
        {
            mutex.ReleaseMutex();
        }
    }
}