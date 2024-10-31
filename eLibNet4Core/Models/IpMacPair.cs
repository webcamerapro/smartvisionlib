using System.Net;
using eLibNet4Core.Helpers;
using eLibNet4Core.Interfaces;

namespace eLibNet4Core.Models
{
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
            Mac = mac;
        }

        /// <summary>
        ///     IP-адрес.
        /// </summary>
        public IPAddress Ip { get; set; }

        /// <summary>
        ///     MAC-адрес.
        /// </summary>
        public MACAddress Mac { get; set; }

        /// <summary>
        ///     Определяет, равен ли текущий объект другому объекту <see cref="IIpMacPair" />.
        /// </summary>
        /// <param name="other">Другой объект <see cref="IIpMacPair" /> для сравнения.</param>
        /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="other" />; иначе <c>false</c>.</returns>
        public bool Equals(IIpMacPair other) =>
            other != null &&
            Equals(Ip, other.Ip) &&
            Equals(Mac, other.Mac);

        /// <summary>
        ///     Определяет, равен ли текущий объект другому объекту.
        /// </summary>
        /// <param name="other">Другой объект для сравнения.</param>
        /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="other" />; иначе <c>false</c>.</returns>
        public override bool Equals(object other) => Equals(other as IIpMacPair);

        /// <summary>
        ///     Возвращает хэш-код для текущего объекта.
        /// </summary>
        /// <returns>Хэш-код для текущего объекта.</returns>
        public override int GetHashCode() => HashCodeHelper.Combine(nameof(IIpMacPair));
    }
}