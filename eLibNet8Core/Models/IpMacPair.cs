using System.Net;
using eLibNet8Core.Interfaces;

namespace eLibNet8Core.Models;

/// <summary>
///     Класс, представляющий пару IP и MAC адресов.
/// </summary>
public class IpMacPair : IIpMacPair
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="IpMacPair" />.
    /// </summary>
    public IpMacPair() { }

    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="IpMacPair" /> с указанными IP и MAC адресами.
    /// </summary>
    /// <param name="ip">IP-адрес.</param>
    /// <param name="mac">MAC-адрес.</param>
    public IpMacPair(IPAddress ip, MACAddress mac)
    {
        Ip  = ip;
        MAC = mac;
    }

    /// <summary>
    ///     IP-адрес.
    /// </summary>
    public IPAddress? Ip { get; set; }

    /// <summary>
    ///     MAC-адрес.
    /// </summary>
    public MACAddress? MAC { get; set; }

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту <see cref="IIpMacPair" />.
    /// </summary>
    /// <param name="other">Другой объект <see cref="IIpMacPair" /> для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="other" />; иначе <c>false</c>.</returns>
    public bool Equals(IIpMacPair? other) =>
        other is not null &&
        Equals(Ip, other.Ip) &&
        Equals(MAC, other.MAC);

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту.
    /// </summary>
    /// <param name="other">Другой объект для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="other" />; иначе <c>false</c>.</returns>
    public override bool Equals(object? other) => Equals(other as IIpMacPair);

    /// <summary>
    ///     Возвращает хэш-код для текущего объекта.
    /// </summary>
    /// <returns>Хэш-код для текущего объекта.</returns>
    public override int GetHashCode() => HashCode.Combine(nameof(IIpMacPair));
}