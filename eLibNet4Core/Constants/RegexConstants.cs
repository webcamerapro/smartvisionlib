using System.Text.RegularExpressions;

namespace eLibNet4Core.Constants
{
    /// <summary>
    ///     Класс регулярных выражений.
    /// </summary>
    public static class RegexConstants
    {
        /// <summary>
        ///     Регулярное выражение: только цифры.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения, соответствующего только цифрам.</returns>
        public static readonly Regex NumberRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase);

        /// <summary>
        ///     Регулярное выражение: IP адрес.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения, соответствующего IP адресу.</returns>
        public static readonly Regex IpRegex = new Regex(@"^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\\b){4}$", RegexOptions.IgnoreCase);

        /// <summary>
        ///     Регулярное выражение: MAC адрес.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения, соответствующего MAC адресу.</returns>
        public static readonly Regex MacRegex = new Regex("^((([a-zA-z0-9]{2}[-:]){5}([a-zA-z0-9]{2}))|(([a-zA-z0-9]{2}:){5}([a-zA-z0-9]{2})))$", RegexOptions.IgnoreCase);

        /// <summary>
        ///     Регулярное выражение: Пара Ip и Mac для Arp.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения, соответствующего только паре Ip и Mac для Arp.</returns>
        public static readonly Regex IpMacPairRegex = new Regex(@"(?<ip>((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4})\s*(?<mac>((([a-zA-z0-9]{2}[-:]){5}([a-zA-z0-9]{2}))|(([a-zA-z0-9]{2}:){5}([a-zA-z0-9]{2}))))", RegexOptions.IgnoreCase);

        /// <summary>
        ///     Регулярное выражение: Протокол и порт.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения, соответствующего протоколу и порту.</returns>
        public static readonly Regex ProtocolPortRegex = new Regex(@"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/", RegexOptions.IgnoreCase);
    }
}