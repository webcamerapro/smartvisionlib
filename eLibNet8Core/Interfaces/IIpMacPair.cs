using System.Net;
using eLibNet8Core.Models;

namespace eLibNet8Core.Interfaces;

/// <summary>
///     Интерфейс, представляющий пару IP и MAC адресов.
/// </summary>
public interface IIpMacPair : IEquatable<IIpMacPair>
{
    /// <summary>
    ///     IP-адрес.
    /// </summary>
    public IPAddress? Ip { get; set; }

    /// <summary>
    ///     MAC-адрес.
    /// </summary>
    public MACAddress? MAC { get; set; }
}