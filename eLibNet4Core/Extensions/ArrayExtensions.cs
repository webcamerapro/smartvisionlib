namespace eLibNet4Core.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для массивов.
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
        public static bool IsValidIndex<T>(this T[] target, int index) => target != null && index >= 0 && index < target.Length;
    }
}