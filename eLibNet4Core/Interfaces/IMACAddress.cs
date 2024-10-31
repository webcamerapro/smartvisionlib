using System;

namespace eLibNet4Core.Interfaces
{
    /// <summary>
    ///     Интерфейс, представляющий MAC-адрес.
    /// </summary>
    public interface IMACAddress : IEquatable<IMACAddress>
    {
        /// <summary>
        ///     Возвращает MAC-адрес.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        ///     Возвращает строковое представление MAC-адреса с использованием тире или двоеточий.
        /// </summary>
        /// <param name="useDash">Использовать тире вместо двоеточий.</param>
        /// <returns>Строковое представление MAC-адреса.</returns>
        string ToString(bool useDash);
    }
}