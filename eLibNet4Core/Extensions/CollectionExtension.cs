using System.Collections;

namespace eLibNet4Core.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для <see cref="ICollection" />.
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        ///     Проверяет, является ли указанный индекс допустимым для данной коллекции.
        /// </summary>
        /// <param name="target">Проверяемый массив.</param>
        /// <param name="index">Проверяемый индекс.</param>
        /// <returns>Возвращает <c>true</c> если <paramref name="target" /> не равен <c>null</c> и <paramref name="index" /> находится в пределах допустимого диапазона; иначе <c>false</c>.</returns>
        public static bool IsValidIndex(this ICollection target, int index) => target != null && index >= 0 && index < target.Count;
    }
}