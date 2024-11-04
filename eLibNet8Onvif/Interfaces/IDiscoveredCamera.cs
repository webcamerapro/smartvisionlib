using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using eLibNet8Core.Models;
using odm.core;

namespace eLibNet8Onvif.Interfaces;

/// <summary>
///     Интерфейс, представляющий обнаруженную камеру.
/// </summary>
public interface IDiscoveredCamera : IEquatable<IDiscoveredCamera>, INotifyPropertyChanging, INotifyPropertyChanged
{
    /// <summary>
    ///     IP-адрес.
    /// </summary>
    public IPAddress? IpAddress { get; set; }

    /// <summary>
    ///     MAC-адрес.
    /// </summary>
    public MACAddress? MacAddress { get; set; }

    /// <summary>
    ///     Предоставляет учетные данные для схем проверки подлинности на основе пароля.
    /// </summary>
    public NetworkCredential Credential { get; }

    /// <summary>
    ///     Менеджер сессий.
    /// </summary>
    public NvtSessionFactory SessionFactory { get; }

    /// <summary>
    ///     Наименование.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     Пароль.
    /// </summary>
    public string Password { get; set; }

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

    /// <summary>
    ///     Пытается получить список URI стримов от ONVIF устройства.
    /// </summary>
    /// <param name="connectionUriIndex">Индекс элемента из списка адресов подключения.</param>
    /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp:// username:password@address").</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Возвращает <c>true</c> если удалось получить список URI стримов от ONVIF устройства, даже если он пуст; иначе <c>false</c>.</returns>
    public Task<bool> TryReceivingStreamUrisAsync(int connectionUriIndex, bool addCredentialData, CancellationToken cancellationToken = default);
}