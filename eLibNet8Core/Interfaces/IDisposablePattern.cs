namespace eLibNet8Core.Interfaces;

/// <summary>
///     Интерфейс, представляющий шаблон для освобождения ресурсов.
/// </summary>
public interface IDisposablePattern : IDisposable
{
    /// <summary>
    ///     Возвращает <c>true</c> если объект был освобожден; иначе <c>false</c>.
    /// </summary>
    public bool IsDisposed { get; }

    /// <summary>
    ///     Выбрасывает исключение <see cref="ObjectDisposedException" />, если объект был освобожден.
    /// </summary>
    public void ThrowIfDisposed();
}