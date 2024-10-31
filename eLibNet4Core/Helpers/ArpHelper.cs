using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using eLibNet4Core.Constants;
using eLibNet4Core.Interfaces;
using eLibNet4Core.Models;

namespace eLibNet4Core.Helpers
{
    /// <summary>
    ///     Вспомогательный класс для работы с ARP.
    /// </summary>
    public static class ArpHelper
    {
        /// <summary>
        ///     Получает MAC-адрес по IP-адресу.
        /// </summary>
        /// <param name="ipAddress">IP-адрес.</param>
        /// <returns>MAC-адрес, если найден; иначе <c>null</c>.</returns>
        public static MACAddress GetMacByIp(IPAddress ipAddress) => (from ipMacPair in GetArpEnumerable() where ipAddress.Equals(ipMacPair.Ip) select ipMacPair.Mac).FirstOrDefault();

        /// <summary>
        ///     Получает MAC-адрес по IP-адресу из указанного перечисления ARP.
        /// </summary>
        /// <param name="ipAddress">IP-адрес.</param>
        /// <param name="arpEnumerable">Перечисление ARP.</param>
        /// <returns>MAC-адрес, если найден; иначе <c>null</c>.</returns>
        public static MACAddress GetMacByIp(IPAddress ipAddress, IEnumerable<IIpMacPair> arpEnumerable) => (from ipMacPair in arpEnumerable where ipAddress.Equals(ipMacPair.Ip) select ipMacPair.Mac).FirstOrDefault();

        /// <summary>
        ///     Получает IP-адрес по MAC-адресу.
        /// </summary>
        /// <param name="macAddress">MAC-адрес.</param>
        /// <returns>IP-адрес, если найден; иначе <c>null</c>.</returns>
        public static IPAddress GetIpByMac(MACAddress macAddress) => (from ipMacPair in GetArpEnumerable() where macAddress.Equals(ipMacPair.Mac) select ipMacPair.Ip).FirstOrDefault();

        /// <summary>
        ///     Получает IP-адрес по MAC-адресу из указанного перечисления ARP.
        /// </summary>
        /// <param name="macAddress">MAC-адрес.</param>
        /// <param name="arpEnumerable">Перечисление ARP.</param>
        /// <returns>IP-адрес, если найден; иначе <c>null</c>.</returns>
        public static IPAddress GetIpByMac(MACAddress macAddress, IEnumerable<IIpMacPair> arpEnumerable) => (from ipMacPair in arpEnumerable where macAddress.Equals(ipMacPair.Mac) select ipMacPair.Ip).FirstOrDefault();

        /// <summary>
        ///     Возвращает перечисление ARP.
        /// </summary>
        /// <returns>Перечисление ARP.</returns>
        public static IEnumerable<IIpMacPair> GetArpEnumerable()
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName               = "arp";
                process.StartInfo.Arguments              = "-a";
                process.StartInfo.UseShellExecute        = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow         = true;
                process.Start();
                foreach (Match match in RegexConstants.IpMacPairRegex.Matches(process.StandardOutput.ReadToEnd()))
                    yield return new IpMacPair(IPAddress.Parse(match.Groups["ip"].Value), MACAddress.Parse(match.Groups["mac"].Value));
            }
        }
    }
}