using System.Text.RegularExpressions;

namespace eLibNet8Onvif.Constants;

/// <summary>
///     Класс регулярных выражений.
/// </summary>
internal static partial class RegexConstants
{
    /// <summary>
    ///     Регулярное выражение проверки URI для ONVIF устройств. Поддерживает протоколы onvif, onvifs, ftp.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения проверки URI для ONVIF устройств.</returns>
    [GeneratedRegex(@"^((onvif[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$", RegexOptions.IgnoreCase, 500, "ru-RU")]
    internal static partial Regex OnvifUriRegex();

    /// <summary>
    ///     Регулярное выражение поиска ONVIF устройства.
    /// </summary>
    /// <returns>Экземпляр регулярного выражения поиска модели ONVIF устройства.</returns>
    [GeneratedRegex("(?<=hardware/).*?(?= )", RegexOptions.IgnoreCase, 500, "ru-RU")]
    internal static partial Regex OnvifHardwareRegex();
}