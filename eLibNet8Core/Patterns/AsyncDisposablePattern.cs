using eLibNet8Core.Helpers;
using eLibNet8Core.Interfaces;

namespace eLibNet8Core.Patterns;

/// <summary>
///     Абстрактный класс-паттерн для <see cref="IAsyncDisposable" />. Также реализует <see cref="IDisposable" />.
/// </summary>
public abstract class AsyncDisposablePattern : IAsyncDisposablePattern
{
    /// <summary>
    ///     Возвращает <c>true</c> если объект был освобожден; иначе <c>false</c>.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    ///     Асинхронно освобождает ресурсы, используемые объектом.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию освобождения ресурсов.</returns>
    public async ValueTask DisposeAsync()
    {
        if (IsDisposed)
            return;
        await DisposeManagedResourcesAsync().ConfigureAwait(false);
        await DisposeUnmanagedResourcesAsync().ConfigureAwait(false);
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Освобождает ресурсы, используемые объектом.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Выбрасывает исключение <see cref="ObjectDisposedException" />, если объект был освобожден.
    /// </summary>
    public void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(IsDisposed, GetType());

    /// <summary>
    ///     Деструктор.
    /// </summary>
    ~AsyncDisposablePattern() => Dispose(false);

    /// <summary>
    ///     Освобождает ресурсы, используемые объектом. Вызывается из Dispose или деструктора.
    /// </summary>
    /// <param name="disposing">Значение, указывающее, вызывается ли метод из Dispose или деструктора.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;
        if (disposing) AsyncHelper.RunAsyncAndReturnToCurrentThread(DisposeManagedResourcesAsync);
        AsyncHelper.RunAsyncAndReturnToCurrentThread(DisposeUnmanagedResourcesAsync);
        IsDisposed = true;
    }

    /// <summary>
    ///     Асинхронно освобождает управляемые ресурсы. Вызывается из <see cref="DisposeAsync" /> и синхронно из Dispose.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию освобождения управляемых ресурсов.</returns>
    protected abstract ValueTask DisposeManagedResourcesAsync();

    /// <summary>
    ///     Асинхронно освобождает неуправляемые ресурсы. Вызывается из <see cref="DisposeAsync" /> и синхронно из Dispose.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию освобождения неуправляемых ресурсов.</returns>
    protected abstract ValueTask DisposeUnmanagedResourcesAsync();
}