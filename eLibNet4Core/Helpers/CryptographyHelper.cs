using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using eLibNet4Core.Extensions;

namespace eLibNet4Core.Helpers
{
    /// <summary>
    ///     Класс, предоставляющий методы для выполнения криптографических операций, включая хэширование и преобразование данных в шестнадцатеричный формат и обратно.
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary>
        ///     Определяет тип пароля на основе его длины. Менее 40 символов - не шифрованный пароль. 40 символов - SHA1. 64 символа - SHA256.
        /// </summary>
        /// <param name="password">Пароль для проверки.</param>
        /// <returns>Тип пароля: 0 (менее 40 символов), 1 (40 символов), 2 (64 символа) или -1, если длина не соответствует ни одному из типов.</returns>
        public static int GetPasswordType(string password)
        {
            if (password.Length < 40)
                return 0;
            switch (password.Length)
            {
                case 40:
                    return 1;
                case 64:
                    return 2;
                default:
                    return -1;
            }
        }

        /// <summary>
        ///     Вычисляет SHA256-хэш текста с высокой степенью безопасности и возвращает его в шестнадцатеричном формате.
        /// </summary>
        /// <param name="text">Текст для хэширования.</param>
        /// <param name="salt">Соль для хэширования (необязательно).</param>
        /// <returns>SHA256-хэш текста в шестнадцатеричном формате.</returns>
        public static string HexSha256HighSecurity(string text, string salt = "")
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt.IsEmpty() ? "Qr4P82XfL9Rr2pQ9" : salt + "2pQ9");
            using (var sha256 = SHA256.Create())
            {
                var textHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                var concat   = new byte[textHash.Length + saltBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, concat, 0, saltBytes.Length);
                Buffer.BlockCopy(textHash, 0, concat, saltBytes.Length, textHash.Length);
                return BytesToHex(sha256.ComputeHash(concat));
            }
        }

        /// <summary>
        ///     Вычисляет SHA256-хэш текста и возвращает его в шестнадцатеричном формате.
        /// </summary>
        /// <param name="text">Текст для хэширования.</param>
        /// <param name="salt">Соль для хэширования (необязательно).</param>
        /// <returns>SHA256-хэш текста в шестнадцатеричном формате.</returns>
        public static string HexSha256(string text, string salt = "")
        {
            using (var sha256 = SHA256.Create())
                return BytesToHex(sha256.ComputeHash(Encoding.UTF8.GetBytes(text + (salt.IsEmpty() ? "Qr4P82XfL9Rr2pQ9" : salt + "2pQ9")))).ToLower();
        }

        /// <summary>
        ///     Вычисляет SHA1-хэш текста и возвращает его в шестнадцатеричном формате.
        /// </summary>
        /// <param name="text">Текст для хэширования.</param>
        /// <param name="salt">Соль для хэширования (необязательно).</param>
        /// <returns>SHA1-хэш текста в шестнадцатеричном формате.</returns>
        public static string HexSha1(string text, string salt = "")
        {
            using (var sha1 = SHA1.Create())
                return BytesToHex(sha1.ComputeHash(Encoding.UTF8.GetBytes(text + (salt.IsEmpty() ? "rf" : salt)))).ToLower();
        }

        /// <summary>
        ///     Преобразует массив байт в строку шестнадцатеричного формата.
        /// </summary>
        /// <param name="bytes">Массив байт для преобразования.</param>
        /// <returns>Строка в шестнадцатеричном формате.</returns>
        public static string BytesToHex(byte[] bytes) => bytes.Aggregate(string.Empty,
            (current, item) => current + $"{(char)(0x37 + (item >> 0x4) + ((((item >> 0x4) - 0xA) >> 0x1F) & -0x7))}{(char)(0x37 + (item & 0xF) + ((((item & 0xF) - 0xA) >> 0x1F) & -0x7))}");

        /// <summary>
        ///     Преобразует строку шестнадцатеричного формата в массив байт.
        /// </summary>
        /// <param name="hex">Строка шестнадцатеричного формата.</param>
        /// <returns>Массив байт.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если длина строки нечетная.</exception>
        public static byte[] HexToBytes(string hex)
        {
            if (hex.Length % 2 != 0) throw new ArgumentException($"Неверная длина строки: {hex.Length}", nameof(hex));
            var offset = hex.StartsWith("0x") ? 0x2 : 0x0;
            var ret    = new byte[(hex.Length - offset) / 0x2];
            for (var i = 0; i < ret.Length; i++)
            {
                ret[i] =  (byte)((HexToDec(hex[offset]) << 0x4) | HexToDec(hex[offset + 0x1]));
                offset += 0x2;
            }

            return ret;
        }

        /// <summary>
        ///     Вычисляет MD5-хэш текста и возвращает его в шестнадцатеричном формате.
        /// </summary>
        /// <param name="text">Текст для хэширования.</param>
        /// <returns>MD5-хэш текста в шестнадцатеричном формате.</returns>
        public static string TextToHexMd5Hash(string text)
        {
            using (var md5 = MD5.Create())
                return BytesToHex(md5.ComputeHash(new ASCIIEncoding().GetBytes(text)));
        }

        private static int HexToDec(char digit)
        {
            switch (digit)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return digit - '0';
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return digit - 'A' + 10;
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                    return digit - 'a' + 10;
                default:
                    throw new ArgumentException($"Символ не соответствует шестнадцатеричной системе счисления: {digit}", nameof(digit));
            }
        }
    }
}