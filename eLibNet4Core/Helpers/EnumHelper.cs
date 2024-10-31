using System;
using System.Collections.Generic;
using System.Linq;
using eLibNet4Core.Extensions;

namespace eLibNet4Core.Helpers
{
    /// <summary>
    ///     Класс, содержащий методы для работы с перечислениями, включая проверку определенности значений и получение всех значений перечисления.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        ///     Проверяет, определено ли перечисление с указанным именем.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="name">Имя для проверки.</param>
        /// <returns><c>True</c>, если перечисление определено; иначе <c>false</c>.</returns>
        public static bool IsDefined<T>(string name) where T : struct, Enum => !name.IsEmpty() && Enum.IsDefined(typeof(T), name);

        /// <summary>
        ///     Проверяет, определено ли перечисление с указанным значением.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="value">Значение для проверки.</param>
        /// <returns><c>True</c>, если перечисление определено; иначе <c>false</c>.</returns>
        public static bool IsDefined<T>(int? value) where T : struct, Enum => value != null && Enum.IsDefined(typeof(T), value);

        /// <summary>
        ///     Возвращает все значения перечисления.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <returns>Список всех значений типа <typeparamref name="T" />.</returns>
        public static IEnumerable<T> GetValues<T>() where T : struct, Enum => Enum.GetValues(typeof(T)).Cast<T>();
    }
}