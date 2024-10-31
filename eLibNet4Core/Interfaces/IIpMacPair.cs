using System;
using System.Net;
using eLibNet4Core.Models;

namespace eLibNet4Core.Interfaces
{
    /// <summary>
    ///     Интерфейс, представляющий пару IP и MAC адресов.
    /// </summary>
    public interface IIpMacPair : IEquatable<IIpMacPair>
    {
        /// <summary>
        ///     IP-адрес.
        /// </summary>
        IPAddress Ip { get; set; }

        /// <summary>
        ///     MAC-адрес.
        /// </summary>
        MACAddress Mac { get; set; }
    }
}