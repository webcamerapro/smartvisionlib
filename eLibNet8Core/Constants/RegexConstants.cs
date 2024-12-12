using System.Text.RegularExpressions;

namespace eLibNet8Core.Constants;

/// <summary>
///     Класс регулярных выражений.
/// </summary>
public static partial class RegexConstants
{
    /// <summary>
    ///     Регулярное выражение: только цифры.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения, соответствующего только цифрам.</returns>
    [GeneratedRegex("^\\d+$", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex NumberRegex();

    /// <summary>
    ///     Регулярное выражение: IP адрес.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения, соответствующего IP адресу.</returns>
    [GeneratedRegex(@"^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\\b){4}$", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex IpRegex();

    /// <summary>
    ///     Регулярное выражение: MAC адрес.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения, соответствующего MAC адресу.</returns>
    [GeneratedRegex("^((([a-zA-z0-9]{2}[-:]){5}([a-zA-z0-9]{2}))|(([a-zA-z0-9]{2}:){5}([a-zA-z0-9]{2})))$", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex MacRegex();

    /// <summary>
    ///     Регулярное выражение: Пара Ip и Mac для Arp.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения, соответствующего только паре Ip и Mac для Arp.</returns>
    [GeneratedRegex(@"(?<ip>((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4})\s*(?<mac>((([a-zA-z0-9]{2}[-:]){5}([a-zA-z0-9]{2}))|(([a-zA-z0-9]{2}:){5}([a-zA-z0-9]{2}))))", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex IpMacPairRegex();

    /// <summary>
    ///     Регулярное выражение: Протокол и порт.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения, соответствующего протоколу и порту.</returns>
    [GeneratedRegex(@"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex ProtocolPortRegex();
    
    /// <summary>
    ///     Имя пользователя и пароль из RTSP ссылки.
    /// </summary>
    /// <returns>Имя пользователя и пароль.</returns>
    [GeneratedRegex("rtsp://(?<username>[^:]+):(?<password>[^@]+)@", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex UsernamePasswordFromRtspRegex();
    
    /// <summary>
    ///     Имя пользователя и пароль из RTSP ссылки. Альтернативный вариант.
    /// </summary>
    /// <returns>Имя пользователя и пароль.</returns>
    [GeneratedRegex("user=(?<username>[^_]+)_password=(?<password>[^_]+)", RegexOptions.IgnoreCase, "ru-RU")]
    public static partial Regex UsernamePasswordFromRtspAltRegex();
}