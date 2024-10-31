using System.Diagnostics.CodeAnalysis;

namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы-расширения для <see cref="bool" />.
/// </summary>
public static class BooleanExtensions
{
    /// <summary>
    ///     Проверяет, является ли значение <see cref="bool" /> истинным.
    /// </summary>
    /// <param name="target">Проверяемое значение <see cref="bool" />.</param>
    /// <returns>Возвращает <c>true</c> если значение <paramref name="target" /> истинно; иначе <c>false</c>.</returns>
    public static bool IsTrue([NotNullWhen(true)] this bool? target) => target is true;

    /// <summary>
    ///     Проверяет, является ли значение <see cref="bool" /> ложным или равным <c>null</c>.
    /// </summary>
    /// <param name="target">Проверяемое значение <see cref="bool" />.</param>
    /// <returns>Возвращает <c>true</c> если значение <paramref name="target" /> ложно или равно <c>null</c>; иначе <c>false</c>.</returns>
    public static bool IsFalse([NotNullWhen(false)] this bool? target) => target is false or null;
}