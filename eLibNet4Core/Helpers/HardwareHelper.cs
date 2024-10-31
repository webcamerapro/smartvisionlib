using System;
using System.Management;
using eLibNet4Core.Extensions;

namespace eLibNet4Core.Helpers
{
    /// <summary>
    ///     Класс, содержащий методы для работы с аппаратным обеспечением, включая получение уникальных идентификаторов.
    /// </summary>
    public static class HardwareHelper
    {
        /// <summary>
        ///     Получает GUID локальной машины на основе хэша MD5 от идентификаторов процессора, БИОС'а и материнской платы.
        /// </summary>
        /// <returns><see cref="Guid" /> локальной машины.</returns>
        public static Guid GetLocalMachineGuid() => new Guid(CryptographyHelper.TextToHexMd5Hash("CPU >> " + CpuId() + "\nBIOS >> " + BiosId() + "\nBASE >> " + BaseId()));

        /// <summary>
        ///     Конвертирует guid устройства в code.
        /// </summary>
        /// <param name="guid">Guid устройства.</param>
        /// <returns>Возвращает <see cref="string" /> code устройства.</returns>
        public static string ConvertGuidToCode(Guid guid)
        {
            var result = "";
            var data   = guid.ToString().Split('-');
            for (var n = 0; n < data.Length; n++)
                result += n == 0 ? MathHelper.BaseConvert(data[n], 16, 36) : '-' + MathHelper.BaseConvert(data[n], 16, 36);
            return result;
        }

        /// <summary>
        ///     Конвертирует code устройства в guid.
        /// </summary>
        /// <param name="code">Code устройства.</param>
        /// <returns>Возвращает <see cref="Guid" /> устройства.</returns>
        public static Guid ConvertCodeToGuid(string code)
        {
            var result = "";
            var data   = code.Split('-');
            for (var n = 0; n < data.Length; n++)
                result += n == 0 ? MathHelper.BaseConvert(data[n], 36, 16) : '-' + MathHelper.BaseConvert(data[n], 36, 16);
            return new Guid(result);
        }

        /// <summary>
        ///     Получает значение свойства Windows Management Instrumentation (WMI) для указанного класса и свойства.
        /// </summary>
        /// <param name="wmiClass">Класс WMI.</param>
        /// <param name="wmiProperty">Свойство класса WMI.</param>
        /// <returns>Значение свойства <paramref name="wmiProperty" /> класса <paramref name="wmiClass" />. Если ничего не найдено, возвращает <see cref="string.Empty" /> ("").</returns>
        private static string GetWmiProperty(string wmiClass, string wmiProperty)
        {
            foreach (var mo in new ManagementClass(wmiClass).GetInstances())
                try
                {
                    var result = mo[wmiProperty].ToString();
                    if (!result.IsEmpty())
                        return result;
                } catch
                {
                    // ignored
                }

            return string.Empty;
        }

        /// <summary>
        ///     Получаем более-менее уникальный идентификатор процессора.
        /// </summary>
        /// <returns>Идентификатор состоящий из UniqueId, ProcessorId, Manufacturer, Name и MaxClockSpeed процессора.</returns>
        private static string CpuId() =>
            GetWmiProperty("Win32_Processor", "UniqueId") +
            GetWmiProperty("Win32_Processor", "ProcessorId") +
            GetWmiProperty("Win32_Processor", "Manufacturer") +
            GetWmiProperty("Win32_Processor", "Name") +
            GetWmiProperty("Win32_Processor", "MaxClockSpeed");

        /// <summary>
        ///     Получаем более-менее уникальный идентификатор БИОС'а.
        /// </summary>
        /// <returns>Идентификатор состоящий из Manufacturer, SMBIOSBIOSVersion, IdentificationCode, SerialNumber, ReleaseDate и Version БИОС'а.</returns>
        private static string BiosId() =>
            GetWmiProperty("Win32_BIOS", "Manufacturer") +
            GetWmiProperty("Win32_BIOS", "SMBIOSBIOSVersion") +
            GetWmiProperty("Win32_BIOS", "IdentificationCode") +
            GetWmiProperty("Win32_BIOS", "SerialNumber") +
            GetWmiProperty("Win32_BIOS", "ReleaseDate") +
            GetWmiProperty("Win32_BIOS", "Version");

        /// <summary>
        ///     Получаем более-менее уникальный идентификатор материнской платы.
        /// </summary>
        /// <returns>Идентификатор состоящий из Model, Manufacturer, Name и SerialNumber материнской платы.</returns>
        private static string BaseId() =>
            GetWmiProperty("Win32_BaseBoard", "Model") +
            GetWmiProperty("Win32_BaseBoard", "Manufacturer") +
            GetWmiProperty("Win32_BaseBoard", "Name") +
            GetWmiProperty("Win32_BaseBoard", "SerialNumber");
    }
}