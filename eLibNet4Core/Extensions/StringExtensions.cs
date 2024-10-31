namespace eLibNet4Core.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Проверяет, является ли строка <c>null</c> или <see cref="string.Empty" /> ("").
        /// </summary>
        /// <param name="sender">Вызывающая строка.</param>
        /// <returns>Возвращает <c>true</c> если значение <paramref name="sender" /> равно <c>null</c> или <see cref="string.Empty" /> (""); иначе <c>false</c>.</returns>
        public static bool IsEmpty(this string sender) => string.IsNullOrEmpty(sender);
    }
}