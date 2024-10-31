using System.Security.Cryptography;
using System.Text;
using eLibNet8Core.Extensions;

namespace eLibNet8Core.Helpers;

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
        return password.Length switch
        {
            < 40 => 0,
            40   => 1,
            64   => 2,
            _    => -1
        };
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
        var textHash  = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        var concat    = new byte[textHash.Length + saltBytes.Length];
        Buffer.BlockCopy(saltBytes, 0, concat, 0, saltBytes.Length);
        Buffer.BlockCopy(textHash, 0, concat, saltBytes.Length, textHash.Length);
        return BytesToHex(SHA256.HashData(concat));
    }

    /// <summary>
    ///     Вычисляет SHA256-хэш текста и возвращает его в шестнадцатеричном формате.
    /// </summary>
    /// <param name="text">Текст для хэширования.</param>
    /// <param name="salt">Соль для хэширования (необязательно).</param>
    /// <returns>SHA256-хэш текста в шестнадцатеричном формате.</returns>
    public static string HexSha256(string text, string salt = "") => BytesToHex(SHA256.HashData(Encoding.UTF8.GetBytes(text + (salt.IsEmpty() ? "Qr4P82XfL9Rr2pQ9" : salt + "2pQ9")))).ToLower();

    /// <summary>
    ///     Вычисляет SHA1-хэш текста и возвращает его в шестнадцатеричном формате.
    /// </summary>
    /// <param name="text">Текст для хэширования.</param>
    /// <param name="salt">Соль для хэширования (необязательно).</param>
    /// <returns>SHA1-хэш текста в шестнадцатеричном формате.</returns>
    public static string HexSha1(string text, string salt = "") => BytesToHex(SHA1.HashData(Encoding.UTF8.GetBytes(text + (salt.IsEmpty() ? "rf" : salt)))).ToLower();

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
    public static string TextToHexMd5Hash(string text) => BytesToHex(MD5.HashData(new ASCIIEncoding().GetBytes(text)));

    private static int HexToDec(char digit) => digit switch
    {
        '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' => digit - '0',
        'A' or 'B' or 'C' or 'D' or 'E' or 'F'                             => digit - 'A' + 10,
        'a' or 'b' or 'c' or 'd' or 'e' or 'f'                             => digit - 'a' + 10,
        _                                                                  => throw new ArgumentException($"Символ не соответствует шестнадцатеричной системе счисления: {digit}", nameof(digit))
    };
}