using System;
using System.Text.RegularExpressions;

namespace eLibNet4Onvif.Constants
{
    /// <summary>
    ///     Класс регулярных выражений.
    /// </summary>
    public static class RegexConstants
    {
        /// <summary>
        ///     Регулярное выражение проверки URI для ONVIF устройств. Поддерживает протоколы onvif, onvifs, ftp.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения проверки URI для ONVIF устройств.</returns>
        public static readonly Regex OnvifUriRegex = new Regex(@"^((onvif[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));

        /// <summary>
        ///     Регулярное выражение поиска ONVIF устройства.
        /// </summary>
        /// <returns>Экземпляр регулярного выражения поиска модели ONVIF устройства.</returns>
        public static readonly Regex OnvifHardwareRegex = new Regex("(?<=hardware/).*?(?= )", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));
    }
}