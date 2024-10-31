using System;
using eLibNet4Core.Constants;
using eLibNet4Core.Helpers;
using eLibNet4Core.Interfaces;

namespace eLibNet4Core.Models
{
    /// <summary>
    ///     Класс, представляющий MAC-адрес.
    /// </summary>
    public class MACAddress : IMACAddress
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="MACAddress" /> с указанной строкой MAC-адреса.
        /// </summary>
        /// <param name="macString">Строка MAC-адреса.</param>
        /// <exception cref="FormatException">Неверный формат MAC-адреса.</exception>
        /// <exception cref="ArgumentOutOfRangeException">MAC должен быть меньше или равен 0xFFFFFFFFFFFF.</exception>
        public MACAddress(string macString)
        {
            if (!TryParse(macString, out ulong? macULong))
                throw new FormatException("Неверный формат MAC-адреса.");
            if (macULong > 0xFFFFFFFFFFFF)
                throw new ArgumentOutOfRangeException(nameof(macString), "MAC должен быть меньше или равен 0xFFFFFFFFFFFF.");
            Address = macULong ?? 0x0;
        }

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="MACAddress" /> с указанным массивом байтов MAC-адреса.
        /// </summary>
        /// <param name="mac8Byte">Массив байтов MAC-адреса.</param>
        /// <exception cref="ArgumentException">Размер массива MAC-адреса должен быть равен 8.</exception>
        /// <exception cref="ArgumentOutOfRangeException">MAC должен быть меньше или равен 0xFFFFFFFFFFFF.</exception>
        public MACAddress(byte[] mac8Byte)
        {
            if (mac8Byte.Length != 8)
                throw new ArgumentException("Размер массива MAC-адреса должен быть равен 8.", nameof(mac8Byte));
            var macULong = BitConverter.ToUInt64(mac8Byte, 0);
            if (macULong > 0xFFFFFFFFFFFF)
                throw new ArgumentOutOfRangeException(nameof(mac8Byte), "MAC должен быть меньше или равен 0xFFFFFFFFFFFF.");
            Address = macULong;
        }

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="MACAddress" /> с указанным значением MAC-адреса.
        /// </summary>
        /// <param name="macULong">Значение MAC-адреса.</param>
        /// <exception cref="ArgumentOutOfRangeException">MAC должен быть меньше или равен 0xFFFFFFFFFFFF.</exception>
        public MACAddress(ulong macULong)
        {
            if (macULong > 0xFFFFFFFFFFFF)
                throw new ArgumentOutOfRangeException(nameof(macULong), "MAC должен быть меньше или равен 0xFFFFFFFFFFFF.");
            Address = macULong;
        }

        /// <summary>
        ///     Возвращает MAC-адрес.
        /// </summary>
        public ulong Address { get; }

        /// <summary>
        ///     Определяет, равен ли текущий объект другому объекту <see cref="IMACAddress" />.
        /// </summary>
        /// <param name="other">Другой объект <see cref="IMACAddress" /> для сравнения.</param>
        /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="other" />; иначе <c>false</c>.</returns>
        public bool Equals(IMACAddress other)
        {
            if (other == null)
                return false;
            return Address == other.Address;
        }

        /// <summary>
        ///     Возвращает строковое представление MAC-адреса с использованием тире или двоеточий.
        /// </summary>
        /// <param name="useDash">Использовать тире вместо двоеточий.</param>
        /// <returns>Строковое представление MAC-адреса.</returns>
        public string ToString(bool useDash)
        {
            var mac8Byte = BitConverter.GetBytes(Address);
            return !useDash ? ToString() : $"{mac8Byte[0]:X2}-{mac8Byte[1]:X2}-{mac8Byte[2]:X2}-{mac8Byte[3]:X2}-{mac8Byte[4]:X2}-{mac8Byte[5]:X2}";
        }

        /// <summary>
        ///     Определяет, равен ли текущий объект другому объекту.
        /// </summary>
        /// <param name="other">Другой объект для сравнения.</param>
        /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="other" />; иначе <c>false</c>.</returns>
        public override bool Equals(object other) => Equals(other as IMACAddress);

        /// <summary>
        ///     Возвращает хэш-код для текущего объекта.
        /// </summary>
        /// <returns>Хэш-код для текущего объекта.</returns>
        public override int GetHashCode() => HashCodeHelper.Combine(Address);

        /// <summary>
        ///     Преобразует строку MAC-адреса в объект <see cref="MACAddress" />.
        /// </summary>
        /// <param name="mac">Строка MAC-адреса.</param>
        /// <returns>Возвращает объект<see cref="MACAddress" />.</returns>
        /// <exception cref="FormatException">Неверный формат MAC-адреса.</exception>
        public static MACAddress Parse(string mac)
        {
            if (!TryParse(mac, out ulong? macULong))
                throw new FormatException("Неверный формат MAC-адреса.");
            return new MACAddress(macULong ?? 0x0);
        }

        /// <summary>
        ///     Преобразует строку MAC-адреса в объект <see cref="IMACAddress" />.
        /// </summary>
        /// <param name="mac">Строка MAC-адреса.</param>
        /// <param name="imacString">Результирующий объект <see cref="IMACAddress" />.</param>
        /// <returns>Возвращает <c>true</c>, если преобразование прошло успешно; иначе <c>false</c>.</returns>
        public static bool TryParse(string mac, out IMACAddress imacString)
        {
            imacString = null;
            if (!TryParse(mac, out ulong? macULong))
                return false;
            imacString = new MACAddress(macULong ?? 0x0);
            return true;
        }

        /// <summary>
        ///     Преобразует строку MAC-адреса в значение <see cref="ulong" />.
        /// </summary>
        /// <param name="mac">Строка MAC-адреса.</param>
        /// <param name="macULong">Результирующее значение <see cref="ulong" />.</param>
        /// <returns>Возвращает <c>true</c>, если преобразование прошло успешно; иначе <c>false</c>.</returns>
        public static bool TryParse(string mac, out ulong? macULong)
        {
            macULong = null;
            if (!RegexConstants.MacRegex.Match(mac).Success)
                return false;
            var macHex = mac.Replace(":", "").Replace("-", "");
            if (macHex.Length != 12)
                return false;
            var result = Convert.ToUInt64(macHex, 16);
            if (result > 0xFFFFFFFFFFFF)
                return false;
            macULong = result;
            return true;
        }

        /// <summary>
        ///     Преобразует строку MAC-адреса в массив байтов.
        /// </summary>
        /// <param name="mac">Строка MAC-адреса.</param>
        /// <param name="mac8Byte">Результирующий массив байтов.</param>
        /// <returns>Возвращает <c>true</c>, если преобразование прошло успешно; иначе <c>false</c>.</returns>
        public static bool TryParse(string mac, out byte[] mac8Byte)
        {
            mac8Byte = null;
            if (!TryParse(mac, out ulong? macULong))
                return false;
            mac8Byte = BitConverter.GetBytes(macULong ?? 0x0);
            return true;
        }

        /// <summary>
        ///     Возвращает строковое представление MAC-адреса с использованием двоеточий.
        /// </summary>
        /// <returns>Строковое представление MAC-адреса.</returns>
        public override string ToString()
        {
            var mac8Byte = BitConverter.GetBytes(Address);
            return $"{mac8Byte[0]:X2}:{mac8Byte[1]:X2}:{mac8Byte[2]:X2}:{mac8Byte[3]:X2}:{mac8Byte[4]:X2}:{mac8Byte[5]:X2}";
        }
    }
}