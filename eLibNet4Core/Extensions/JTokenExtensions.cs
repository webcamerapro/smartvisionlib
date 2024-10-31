using Newtonsoft.Json.Linq;

namespace eLibNet4Core.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для <see cref="JToken" />.
    /// </summary>
    public static class JTokenExtensions
    {
        /// <summary>
        ///     Проверяет, является ли <see cref="JToken" /> пустым или равным <c>null</c>.
        /// </summary>
        /// <param name="target">Проверяемый <see cref="JToken" />.</param>
        /// <returns>Возвращает <c>true</c> если <paramref name="target" /> равен <c>null</c>, его тип <see cref="JTokenType.Null" /> или он является пустой строкой; иначе <c>false</c>.</returns>
        public static bool IsEmpty(this JToken target) => target == null || target.Type == JTokenType.Null || (target.Type == JTokenType.String && target.ToString().IsEmpty());
    }
}