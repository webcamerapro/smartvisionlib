using System.Diagnostics.CodeAnalysis;

namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы-расширения для <see cref="Array" />.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    ///     Проверяет, является ли указанный индекс допустимым для данного массива.
    /// </summary>
    /// <typeparam name="T">Тип элементов массива.</typeparam>
    /// <param name="target">Проверяемый массив.</param>
    /// <param name="index">Проверяемый индекс.</param>
    /// <returns>Возвращает <c>true</c> если <paramref name="target" /> не равен <c>null</c> и <paramref name="index" /> находится в пределах допустимого диапазона; иначе <c>false</c>.</returns>
    public static bool IsValidIndex<T>([NotNullWhen(true)] this T[]? target, [NotNullWhen(true)] int? index) => target is not null && index >= 0 && index < target.Length;
}