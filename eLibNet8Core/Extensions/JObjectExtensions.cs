using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы-расширения для <see cref="JObject" />.
/// </summary>
public static class JObjectExtensions
{
    /// <summary>
    ///     Проверяет, является ли <see cref="JObject" /> пустым или равным <c>null</c>.
    /// </summary>
    /// <param name="target">Проверяемый <see cref="JObject" />.</param>
    /// <returns>Возвращает <c>true</c> если <paramref name="target" /> равен <c>null</c>, его тип <see cref="JTokenType.Null" /> или он является пустой строкой; иначе <c>false</c>.</returns>
    public static bool IsEmpty([NotNullWhen(false)] this JObject? target) => target == null || target.Type == JTokenType.Null || (target.Type == JTokenType.String && target.ToString().IsEmpty());
}