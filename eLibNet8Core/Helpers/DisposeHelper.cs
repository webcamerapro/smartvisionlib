using System.Diagnostics.CodeAnalysis;

namespace eLibNet8Core.Helpers;

/// <summary>
///     Класс, предоставляющий вспомогательные методы для освобождения ресурсов объектов.
/// </summary>
public static class DisposeHelper
{
    /// <summary>
    ///     Освобождает ресурсы, занимаемые объектом, и устанавливает его значение по умолчанию.
    /// </summary>
    /// <param name="target">Объект, который нужно освободить и установить по умолчанию.</param>
    public static void DisposeAndDefault<T>(ref T? target)
    {
        if (target is null)
            return;
        if (target is IDisposable disposable)
            disposable.Dispose();
        target = default;
    }

    /// <summary>
    ///     Освобождает ресурсы, занимаемые текущим объектом, и устанавливает новое значение.
    /// </summary>
    /// <param name="target">Объект, который нужно освободить.</param>
    /// <param name="value">Новое значение для установки.</param>
    public static void DisposeAndSet<T>([NotNullIfNotNull(nameof(value))] ref T? target, T? value)
    {
        if (target is not null && target is IDisposable disposable)
            disposable.Dispose();
        target = value;
    }
}