using System.Runtime.CompilerServices;

namespace eLibNet4Core.Helpers
{
    /// <summary>
    ///     Класс, содержащий методы для вычисления хэш-кодов.
    /// </summary>
    public static class HashCodeHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MixFinal(uint hash)
        {
            hash ^= hash >> 15;
            hash *= 2246822519U;
            hash ^= hash >> 13;
            hash *= 3266489917U;
            hash ^= hash >> 16;
            return hash;
        }

        /// <summary>
        ///     Хэш-код одного значения.
        /// </summary>
        /// <typeparam name="T0">Тип значения.</typeparam>
        /// <param name="value0">Значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0>(T0 value0)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды двух значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1>(T0 value0, T1 value1)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды трёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2>(T0 value0, T1 value1, T2 value2)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3>(T0 value0, T1 value1, T2 value2, T3 value3)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <typeparam name="T10">Тип одиннадцатого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <param name="value10">Одиннадцатое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            var  hc10 = (uint)(value10?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = hash * 9176 + hc10;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <typeparam name="T10">Тип одиннадцатого значения.</typeparam>
        /// <typeparam name="T11">Тип двенадцатого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <param name="value10">Одиннадцатое значение.</param>
        /// <param name="value11">Двенадцатое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10, T11 value11)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            var  hc10 = (uint)(value10?.GetHashCode() ?? 0);
            var  hc11 = (uint)(value11?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = hash * 9176 + hc10;
            hash = hash * 9176 + hc11;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <typeparam name="T10">Тип одиннадцатого значения.</typeparam>
        /// <typeparam name="T11">Тип двенадцатого значения.</typeparam>
        /// <typeparam name="T12">Тип тринадцатого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <param name="value10">Одиннадцатое значение.</param>
        /// <param name="value11">Двенадцатое значение.</param>
        /// <param name="value12">Тринадцатое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10, T11 value11, T12 value12)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            var  hc10 = (uint)(value10?.GetHashCode() ?? 0);
            var  hc11 = (uint)(value11?.GetHashCode() ?? 0);
            var  hc12 = (uint)(value12?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = hash * 9176 + hc10;
            hash = hash * 9176 + hc11;
            hash = hash * 9176 + hc12;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <typeparam name="T10">Тип одиннадцатого значения.</typeparam>
        /// <typeparam name="T11">Тип двенадцатого значения.</typeparam>
        /// <typeparam name="T12">Тип тринадцатого значения.</typeparam>
        /// <typeparam name="T13">Тип четырнадцатого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <param name="value10">Одиннадцатое значение.</param>
        /// <param name="value11">Двенадцатое значение.</param>
        /// <param name="value12">Тринадцатое значение.</param>
        /// <param name="value13">Четырнадцатое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10, T11 value11, T12 value12,
            T13 value13)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            var  hc10 = (uint)(value10?.GetHashCode() ?? 0);
            var  hc11 = (uint)(value11?.GetHashCode() ?? 0);
            var  hc12 = (uint)(value12?.GetHashCode() ?? 0);
            var  hc13 = (uint)(value13?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = hash * 9176 + hc10;
            hash = hash * 9176 + hc11;
            hash = hash * 9176 + hc12;
            hash = hash * 9176 + hc13;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <typeparam name="T10">Тип одиннадцатого значения.</typeparam>
        /// <typeparam name="T11">Тип двенадцатого значения.</typeparam>
        /// <typeparam name="T12">Тип тринадцатого значения.</typeparam>
        /// <typeparam name="T13">Тип четырнадцатого значения.</typeparam>
        /// <typeparam name="T14">Тип пятнадцатого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <param name="value10">Одиннадцатое значение.</param>
        /// <param name="value11">Двенадцатое значение.</param>
        /// <param name="value12">Тринадцатое значение.</param>
        /// <param name="value13">Четырнадцатое значение.</param>
        /// <param name="value14">Пятнадцатое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10, T11 value11, T12 value12,
            T13 value13, T14 value14)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            var  hc10 = (uint)(value10?.GetHashCode() ?? 0);
            var  hc11 = (uint)(value11?.GetHashCode() ?? 0);
            var  hc12 = (uint)(value12?.GetHashCode() ?? 0);
            var  hc13 = (uint)(value13?.GetHashCode() ?? 0);
            var  hc14 = (uint)(value14?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = hash * 9176 + hc10;
            hash = hash * 9176 + hc11;
            hash = hash * 9176 + hc12;
            hash = hash * 9176 + hc13;
            hash = hash * 9176 + hc14;
            hash = MixFinal(hash);
            return (int)hash;
        }

        /// <summary>
        ///     Комбинирует хэш-коды четырёх значений.
        /// </summary>
        /// <typeparam name="T0">Тип первого значения.</typeparam>
        /// <typeparam name="T1">Тип второго значения.</typeparam>
        /// <typeparam name="T2">Тип третьего значения.</typeparam>
        /// <typeparam name="T3">Тип четвёртого значения.</typeparam>
        /// <typeparam name="T4">Тип пятого значения.</typeparam>
        /// <typeparam name="T5">Тип шестого значения.</typeparam>
        /// <typeparam name="T6">Тип седьмого значения.</typeparam>
        /// <typeparam name="T7">Тип восьмого значения.</typeparam>
        /// <typeparam name="T8">Тип девятого значения.</typeparam>
        /// <typeparam name="T9">Тип десятого значения.</typeparam>
        /// <typeparam name="T10">Тип одиннадцатого значения.</typeparam>
        /// <typeparam name="T11">Тип двенадцатого значения.</typeparam>
        /// <typeparam name="T12">Тип тринадцатого значения.</typeparam>
        /// <typeparam name="T13">Тип четырнадцатого значения.</typeparam>
        /// <typeparam name="T14">Тип пятнадцатого значения.</typeparam>
        /// <typeparam name="T15">Тип шестнадцатого значения.</typeparam>
        /// <param name="value0">Первое значение.</param>
        /// <param name="value1">Второе значение.</param>
        /// <param name="value2">Третье значение.</param>
        /// <param name="value3">Четвёртое значение.</param>
        /// <param name="value4">Пятое значение.</param>
        /// <param name="value5">Шестое значение.</param>
        /// <param name="value6">Седьмое значение.</param>
        /// <param name="value7">Восьмое значение.</param>
        /// <param name="value8">Девятое значение.</param>
        /// <param name="value9">Десятое значение.</param>
        /// <param name="value10">Одиннадцатое значение.</param>
        /// <param name="value11">Двенадцатое значение.</param>
        /// <param name="value12">Тринадцатое значение.</param>
        /// <param name="value13">Четырнадцатое значение.</param>
        /// <param name="value14">Пятнадцатое значение.</param>
        /// <param name="value15">Шестнадцатое значение.</param>
        /// <returns>Комбинированный хэш-код.</returns>
        public static int Combine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10, T11 value11,
            T12 value12, T13 value13, T14 value14, T15 value15)
        {
            var  hc0  = (uint)(value0?.GetHashCode() ?? 0);
            var  hc1  = (uint)(value1?.GetHashCode() ?? 0);
            var  hc2  = (uint)(value2?.GetHashCode() ?? 0);
            var  hc3  = (uint)(value3?.GetHashCode() ?? 0);
            var  hc4  = (uint)(value4?.GetHashCode() ?? 0);
            var  hc5  = (uint)(value5?.GetHashCode() ?? 0);
            var  hc6  = (uint)(value6?.GetHashCode() ?? 0);
            var  hc7  = (uint)(value7?.GetHashCode() ?? 0);
            var  hc8  = (uint)(value8?.GetHashCode() ?? 0);
            var  hc9  = (uint)(value9?.GetHashCode() ?? 0);
            var  hc10 = (uint)(value10?.GetHashCode() ?? 0);
            var  hc11 = (uint)(value11?.GetHashCode() ?? 0);
            var  hc12 = (uint)(value12?.GetHashCode() ?? 0);
            var  hc13 = (uint)(value13?.GetHashCode() ?? 0);
            var  hc14 = (uint)(value14?.GetHashCode() ?? 0);
            var  hc15 = (uint)(value15?.GetHashCode() ?? 0);
            uint hash = 1009;
            hash = hash * 9176 + hc0;
            hash = hash * 9176 + hc1;
            hash = hash * 9176 + hc2;
            hash = hash * 9176 + hc3;
            hash = hash * 9176 + hc4;
            hash = hash * 9176 + hc5;
            hash = hash * 9176 + hc6;
            hash = hash * 9176 + hc7;
            hash = hash * 9176 + hc8;
            hash = hash * 9176 + hc9;
            hash = hash * 9176 + hc10;
            hash = hash * 9176 + hc11;
            hash = hash * 9176 + hc12;
            hash = hash * 9176 + hc13;
            hash = hash * 9176 + hc14;
            hash = hash * 9176 + hc15;
            hash = MixFinal(hash);
            return (int)hash;
        }
    }
}