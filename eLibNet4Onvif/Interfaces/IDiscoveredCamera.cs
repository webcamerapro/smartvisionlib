using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using eLibNet4Core.Models;

namespace eLibNet4Onvif.Interfaces
{
    /// <summary>
    ///     Интерфейс, представляющий обнаруженную камеру.
    /// </summary>
    public interface IDiscoveredCamera : IEquatable<IDiscoveredCamera>, INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        ///     IP-адрес.
        /// </summary>
        IPAddress IpAddress { get; }

        /// <summary>
        ///     MAC-адрес.
        /// </summary>
        MACAddress MacAddress { get; set; }

        /// <summary>
        ///     Наименование.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Имя пользователя.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        ///     Пароль.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        ///     Производитель.
        /// </summary>
        string Manufacturer { get; set; }

        /// <summary>
        ///     Модель.
        /// </summary>
        string Model { get; set; }

        /// <summary>
        ///     Список Scopes.
        /// </summary>
        ObservableCollection<string> Scopes { get; }

        /// <summary>
        ///     Список типов.
        /// </summary>
        ObservableCollection<string> Types { get; }

        /// <summary>
        ///     Список адресов подключения.
        /// </summary>
        ObservableCollection<string> XAddresses { get; }

        /// <summary>
        ///     Список URI стримов.
        /// </summary>
        ObservableDictionary<string, Uri> StreamUris { get; }

        /// <summary>
        ///     Импортирует список Scopes.
        /// </summary>
        /// <param name="scopes">Список Scopes.</param>
        void ImportScopes(IEnumerable<string> scopes);

        /// <summary>
        ///     Импортирует список типов.
        /// </summary>
        /// <param name="types">Список типов.</param>
        void ImportTypes(IEnumerable<string> types);

        /// <summary>
        ///     Импортирует список адресов подключения.
        /// </summary>
        /// <param name="xAddresses">Список адресов подключения.</param>
        void ImportXAddresses(IEnumerable<string> xAddresses);

        /// <summary>
        ///     Импортирует список URI стримов.
        /// </summary>
        /// <param name="streamUris">Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</param>
        void ImportStreamUris(IEnumerable<KeyValuePair<string, Uri>> streamUris);

        /// <summary>
        ///     Импортирует список URI стримов.
        /// </summary>
        /// <param name="streamUris">Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</param>
        void ImportStreamUris(IEnumerable<KeyValuePair<string, string>> streamUris);
    }
}