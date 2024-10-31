namespace eLibNet4Core.Extensions
{
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
        public static bool IsTrue(this bool? target) => target == true;

        /// <summary>
        ///     Проверяет, является ли значение <see cref="bool" /> ложным или равным <c>null</c>.
        /// </summary>
        /// <param name="target">Проверяемое значение <see cref="bool" />.</param>
        /// <returns>Возвращает <c>true</c> если значение <paramref name="target" /> ложно или равно <c>null</c>; иначе <c>false</c>.</returns>
        public static bool IsFalse(this bool? target) => target == false || target == null;
    }
}