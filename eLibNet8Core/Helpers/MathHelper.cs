using eLibNet8Core.Constants;

namespace eLibNet8Core.Helpers;

/// <summary>
///     Класс, содержащий методы для выполнения различных математических операций, включая конвертацию чисел между различными системами счисления.
/// </summary>
public static class MathHelper
{
    /// <summary>
    ///     Конвертирует строку из одной системы счисления в другую.
    /// </summary>
    /// <param name="value">Строка, представляющая число в исходной системе счисления.</param>
    /// <param name="from">Основание исходной системы счисления.</param>
    /// <param name="to">Основание целевой системы счисления.</param>
    /// <returns>Строка, представляющая число в целевой системе счисления.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если основание системы счисления некорректно.</exception>
    public static string BaseConvert(string value, int from, int to)
    {
        if (from == to)
            return value;
        switch (from)
        {
            case 10 when long.TryParse(value, out var number): return BaseConvertTo(number, to);
            case < 10 when RegexConstants.NumberRegex().IsMatch(value):
            case > 10: return to == 10 ? BaseConvertFrom(value, from).ToString() : BaseConvertTo(BaseConvertFrom(value, from), to);
            default: throw new ArgumentException("Несоответствие выбранной системе счисления");
        }
    }

    /// <summary>
    ///     Конвертирует строку из произвольной системы счисления в десятичную.
    /// </summary>
    /// <param name="value">Строка, представляющая число в исходной системе счисления.</param>
    /// <param name="from">Основание исходной системы счисления.</param>
    /// <returns>Число в десятичной системе счисления.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если основание системы счисления некорректно или строка содержит недопустимые символы.</exception>
    public static long BaseConvertFrom(string value, int from)
    {
        if (from < 2 || from > FieldConstants.SystemChars.Length)
            throw new ArgumentException("Основание должно быть >= 2 и <= " + FieldConstants.SystemChars.Length);
        if (string.IsNullOrEmpty(value))
            return 0;
        value = value.ToLowerInvariant();
        long result     = 0;
        long multiplier = 1;
        for (var i = value.Length - 1; i >= 0; i--)
        {
            var c = value[i];
            if (i == 0 && c == '-')
            {
                result = -result;
                break;
            }

            var digit = FieldConstants.SystemChars.IndexOf(c);
            if (digit == -1)
                throw new ArgumentException("Недопустимый символ в номере произвольной системы счисления", nameof(value));
            result     += digit * multiplier;
            multiplier *= from;
        }

        return result;
    }

    /// <summary>
    ///     Конвертирует число из десятичной системы счисления в произвольную.
    /// </summary>
    /// <param name="value">Число в десятичной системе счисления.</param>
    /// <param name="to">Основание целевой системы счисления.</param>
    /// <returns>Строка, представляющая число в целевой системе счисления.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если основание системы счисления некорректно.</exception>
    public static string BaseConvertTo(long value, int to)
    {
        const int bitsInLong = 64;

        if (to < 2 || to > FieldConstants.SystemChars.Length)
            throw new ArgumentException("Основание должно быть >= 2 и <= " + FieldConstants.SystemChars.Length);

        if (value == 0)
            return "0";

        var index         = bitsInLong - 1;
        var currentNumber = Math.Abs(value);
        var charArray     = new char[bitsInLong];

        while (currentNumber != 0)
        {
            var remainder = (int)(currentNumber % to);
            charArray[index--] =  FieldConstants.SystemChars[remainder];
            currentNumber      /= to;
        }

        var result            = new string(charArray, index + 1, bitsInLong - index - 1);
        if (value < 0) result = "-" + result;

        return result;
    }
}