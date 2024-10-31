using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using eLibNet8Core.Constants;
using eLibNet8Core.Interfaces;
using eLibNet8Core.Models;

namespace eLibNet8Core.Helpers;

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
    public static MACAddress? GetMacByIp(IPAddress ipAddress) => (from ipMacPair in GetArpEnumerable() where ipAddress.Equals(ipMacPair.Ip) select ipMacPair.MAC).FirstOrDefault();

    /// <summary>
    ///     Получает MAC-адрес по IP-адресу из указанного перечисления ARP.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    /// <param name="arpEnumerable">Перечисление ARP.</param>
    /// <returns>MAC-адрес, если найден; иначе <c>null</c>.</returns>
    public static MACAddress? GetMacByIp(IPAddress ipAddress, IEnumerable<IIpMacPair> arpEnumerable) => (from ipMacPair in arpEnumerable where ipAddress.Equals(ipMacPair.Ip) select ipMacPair.MAC).FirstOrDefault();

    /// <summary>
    ///     Асинхронно получает MAC-адрес по IP-адресу.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>MAC-адрес, если найден; иначе <c>null</c>.</returns>
    public static async Task<MACAddress?> GetMacByIpAsync(IPAddress ipAddress, CancellationToken cancellationToken = default)
    {
        await foreach (var ipMacPair in GetArpEnumerableAsync(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (ipAddress.Equals(ipMacPair.Ip))
                return ipMacPair.MAC;
        }

        return null;
    }

    /// <summary>
    ///     Асинхронно получает MAC-адрес по IP-адресу из указанного асинхронного перечисления ARP.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    /// <param name="arpAsyncEnumerable">Асинхронное перечисление ARP.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>MAC-адрес, если найден; иначе <c>null</c>.</returns>
    public static async Task<MACAddress?> GetMacByIpAsync(IPAddress ipAddress, IAsyncEnumerable<IIpMacPair> arpAsyncEnumerable, CancellationToken cancellationToken = default)
    {
        await foreach (var ipMacPair in arpAsyncEnumerable.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (ipAddress.Equals(ipMacPair.Ip))
                return ipMacPair.MAC;
        }

        return null;
    }

    /// <summary>
    ///     Получает IP-адрес по MAC-адресу.
    /// </summary>
    /// <param name="macAddress">MAC-адрес.</param>
    /// <returns>IP-адрес, если найден; иначе <c>null</c>.</returns>
    public static IPAddress? GetIpByMac(MACAddress macAddress) => (from ipMacPair in GetArpEnumerable() where macAddress.Equals(ipMacPair.MAC) select ipMacPair.Ip).FirstOrDefault();

    /// <summary>
    ///     Получает IP-адрес по MAC-адресу из указанного перечисления ARP.
    /// </summary>
    /// <param name="macAddress">MAC-адрес.</param>
    /// <param name="arpEnumerable">Перечисление ARP.</param>
    /// <returns>IP-адрес, если найден; иначе <c>null</c>.</returns>
    public static IPAddress? GetIpByMac(MACAddress macAddress, IEnumerable<IIpMacPair> arpEnumerable) => (from ipMacPair in arpEnumerable where macAddress.Equals(ipMacPair.MAC) select ipMacPair.Ip).FirstOrDefault();

    /// <summary>
    ///     Асинхронно получает IP-адрес по MAC-адресу.
    /// </summary>
    /// <param name="macAddress">MAC-адрес.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>IP-адрес, если найден; иначе <c>null</c>.</returns>
    public static async Task<IPAddress?> GetIpByMacAsync(MACAddress macAddress, CancellationToken cancellationToken = default)
    {
        await foreach (var ipMacPair in GetArpEnumerableAsync(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (macAddress.Equals(ipMacPair.MAC))
                return ipMacPair.Ip;
        }

        return null;
    }

    /// <summary>
    ///     Асинхронно получает IP-адрес по MAC-адресу из указанного асинхронного перечисления ARP.
    /// </summary>
    /// <param name="macAddress">MAC-адрес.</param>
    /// <param name="arpAsyncEnumerable">Асинхронное перечисление ARP.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>IP-адрес, если найден; иначе <c>null</c>.</returns>
    public static async Task<IPAddress?> GetIpByMacAsync(MACAddress macAddress, IAsyncEnumerable<IIpMacPair> arpAsyncEnumerable, CancellationToken cancellationToken = default)
    {
        await foreach (var ipMacPair in arpAsyncEnumerable.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (macAddress.Equals(ipMacPair.MAC))
                return ipMacPair.Ip;
        }

        return null;
    }

    /// <summary>
    ///     Возвращает перечисление ARP.
    /// </summary>
    /// <returns>Перечисление ARP.</returns>
    public static IEnumerable<IIpMacPair> GetArpEnumerable()
    {
        using var process = new Process();
        process.StartInfo.FileName               = "arp";
        process.StartInfo.Arguments              = "-a";
        process.StartInfo.UseShellExecute        = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow         = true;
        process.Start();
        foreach (Match match in RegexConstants.IpMacPairRegex().Matches(process.StandardOutput.ReadToEnd()))
            yield return new IpMacPair(IPAddress.Parse(match.Groups["ip"].Value), MACAddress.Parse(match.Groups["mac"].Value));
    }

    /// <summary>
    ///     Асинхронно возвращает перечисление ARP.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронное перечисление ARP.</returns>
    public static async IAsyncEnumerable<IIpMacPair> GetArpEnumerableAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var process = new Process();
        process.StartInfo.FileName               = "arp";
        process.StartInfo.Arguments              = "-a";
        process.StartInfo.UseShellExecute        = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow         = true;
        process.Start();
        foreach (Match match in RegexConstants.IpMacPairRegex().Matches(await process.StandardOutput.ReadToEndAsync(cancellationToken)))
            yield return new IpMacPair(IPAddress.Parse(match.Groups["ip"].Value), MACAddress.Parse(match.Groups["mac"].Value));
    }
}