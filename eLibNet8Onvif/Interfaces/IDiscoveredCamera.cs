using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using eLibNet8Core.Models;

namespace eLibNet8Onvif.Interfaces;

/// <summary>
///     Интерфейс, представляющий обнаруженную камеру.
/// </summary>
public interface IDiscoveredCamera : IEquatable<IDiscoveredCamera>, INotifyPropertyChanging, INotifyPropertyChanged
{
    /// <summary>
    ///     IP-адрес.
    /// </summary>
    public IPAddress IpAddress { get; }

    /// <summary>
    ///     MAC-адрес.
    /// </summary>
    public MACAddress? MacAddress { get; set; }

    /// <summary>
    ///     Наименование.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    ///     Пароль.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    ///     Производитель.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    ///     Модель.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    ///     Список Scopes.
    /// </summary>
    public ObservableCollection<Uri> Scopes { get; }

    /// <summary>
    ///     Список адресов подключения.
    /// </summary>
    public ObservableCollection<Uri> ConnectionUris { get; }

    /// <summary>
    ///     Список URI стримов.
    /// </summary>
    public ObservableDictionary<string, Uri> StreamUris { get; }

    /// <summary>
    ///     Импортирует список Scopes.
    /// </summary>
    /// <param name="scopes">Список Scopes.</param>
    public void ImportScopes(IEnumerable<Uri> scopes);

    /// <summary>
    ///     Импортирует список адресов подключения.
    /// </summary>
    /// <param name="uris">Список адресов подключения.</param>
    public void ImportConnectionUris(IEnumerable<Uri> uris);

    /// <summary>
    ///     Импортирует список URI стримов.
    /// </summary>
    /// <param name="streamUris">Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</param>
    public void ImportStreamUris(IEnumerable<KeyValuePair<string, Uri>> streamUris);
}