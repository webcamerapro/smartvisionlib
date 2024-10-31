using System.ComponentModel;

namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы-расширения для <see cref="Enum" />.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///     Получает описание для указанного значения перечисления <see cref="Enum" />.
    /// </summary>
    /// <param name="target">Значение перечисления, для которого нужно получить описание.</param>
    /// <returns>Возвращает описание, указанное в атрибуте <see cref="DescriptionAttribute" />, или пустую строку, если атрибут отсутствует.</returns>
    public static string GetDescription(this Enum target) =>
        target.GetType().GetField(target.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] { Length: > 0 } attributes ? attributes[0].Description : string.Empty;
}