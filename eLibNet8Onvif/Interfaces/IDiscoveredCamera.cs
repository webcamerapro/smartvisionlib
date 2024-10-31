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
    public ObservableCollection<string> Scopes { get; }

    /// <summary>
    ///     Список типов.
    /// </summary>
    public ObservableCollection<string> Types { get; }

    /// <summary>
    ///     Список адресов подключения.
    /// </summary>
    public ObservableCollection<string> XAddresses { get; }

    /// <summary>
    ///     Список URI стримов.
    /// </summary>
    public ObservableDictionary<string, Uri> StreamUris { get; }

    /// <summary>
    ///     Импортирует список Scopes.
    /// </summary>
    /// <param name="scopes">Список Scopes.</param>
    public void ImportScopes(IEnumerable<string> scopes);

    /// <summary>
    ///     Импортирует список типов.
    /// </summary>
    /// <param name="types">Список типов.</param>
    public void ImportTypes(IEnumerable<string> types);

    /// <summary>
    ///     Импортирует список адресов подключения.
    /// </summary>
    /// <param name="xAddresses">Список адресов подключения.</param>
    public void ImportXAddresses(IEnumerable<string> xAddresses);

    /// <summary>
    ///     Импортирует список URI стримов.
    /// </summary>
    /// <param name="streamUris">Список URI потоков.</param>
    public void ImportStreamUris(IEnumerable<KeyValuePair<string, Uri>> streamUris);

    /// <summary>
    ///     Импортирует список URI стримов.
    /// </summary>
    /// <param name="streamUris">Список URI потоков.</param>
    public void ImportStreamUris(IEnumerable<KeyValuePair<string, string>> streamUris);
}