namespace eLibNet8Core.Interfaces;

/// <summary>
///     Интерфейс, представляющий MAC-адрес.
/// </summary>
public interface IMACAddress : IEquatable<IMACAddress>
{
    /// <summary>
    ///     Возвращает MAC-адрес.
    /// </summary>
    public ulong Address { get; }

    /// <summary>
    ///     Возвращает строковое представление MAC-адреса с использованием тире или двоеточий.
    /// </summary>
    /// <param name="useDash">Использовать тире вместо двоеточий.</param>
    /// <returns>Строковое представление MAC-адреса.</returns>
    public string ToString(bool useDash);
}