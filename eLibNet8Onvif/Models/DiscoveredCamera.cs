using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using Dasync.Collections;
using eLibNet8Core.Extensions;
using eLibNet8Core.Models;
using eLibNet8Onvif.Helpers;
using eLibNet8Onvif.Interfaces;
using odm.core;

namespace eLibNet8Onvif.Models;

/// <summary>
///     Класс, представляющий обнаруженную камеру.
/// </summary>
public partial class DiscoveredCamera : ObservableObject, IDiscoveredCamera
{
    /// <summary>
    ///     IP-адрес.
    /// </summary>
    [ObservableProperty]
    private IPAddress? _ipAddress;

    /// <summary>
    ///     MAC-адрес.
    /// </summary>
    [ObservableProperty]
    private MACAddress? _macAddress;

    /// <summary>
    ///     Производитель.
    /// </summary>
    [ObservableProperty]
    private string? _manufacturer;

    /// <summary>
    ///     Модель.
    /// </summary>
    [ObservableProperty]
    private string? _model;

    /// <summary>
    ///     Наименование.
    /// </summary>
    [ObservableProperty]
    private string? _title;

    /// <summary>
    ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
    /// </summary>
    public DiscoveredCamera()
    {
        Credential     = new();
        SessionFactory = new(Credential);
    }

    /// <summary>
    ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    public DiscoveredCamera(IPAddress ipAddress) : this() => _ipAddress = ipAddress;

    /// <summary>
    ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    /// <param name="manufacturer">Производитель.</param>
    /// <param name="model">Модель.</param>
    /// <param name="scopes">Список Scopes.</param>
    /// <param name="connectionUris">Список адресов подключения.</param>
    public DiscoveredCamera(IPAddress ipAddress, string manufacturer, string model, IEnumerable<Uri> scopes, IEnumerable<Uri> connectionUris) : this(ipAddress)
    {
        _manufacturer = manufacturer;
        _model        = model;
        foreach (var scope in scopes)
            Scopes.Add(scope);
        foreach (var connectionUri in connectionUris)
            ConnectionUris.Add(connectionUri);
    }

    /// <summary>
    ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    /// <param name="macAddress">MAC-адрес.</param>
    /// <param name="title">Наименование.</param>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль.</param>
    /// <param name="manufacturer">Производитель.</param>
    /// <param name="model">Модель.</param>
    /// <param name="scopes">Список Scopes.</param>
    /// <param name="connectionUris">Список адресов подключения.</param>
    /// <param name="streamUris">Словарь токенов профилей и URI стримов.</param>
    public DiscoveredCamera(IPAddress ipAddress, MACAddress macAddress, string title, string username, string password, string manufacturer, string model, IEnumerable<Uri> scopes, IEnumerable<Uri> connectionUris,
        IEnumerable<KeyValuePair<string, Uri>> streamUris) : this(ipAddress, manufacturer, model, scopes, connectionUris)
    {
        _macAddress         = macAddress;
        _title              = title;
        Credential.UserName = username;
        Credential.Password = password;
        foreach (var streamUri in streamUris)
            StreamUris.Add(streamUri.Key, streamUri.Value);
    }

    /// <summary>
    ///     Предоставляет учетные данные для схем проверки подлинности на основе пароля.
    /// </summary>
    public NetworkCredential Credential { get; }

    /// <summary>
    ///     Менеджер сессий.
    /// </summary>
    public NvtSessionFactory SessionFactory { get; }

    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Username
    {
        get => Credential.UserName;
        set => SetProperty(Credential.UserName, value, Credential, (credential, username) => credential.UserName = username);
    }

    /// <summary>
    ///     Пароль.
    /// </summary>
    public string Password
    {
        get => Credential.Password;
        set => SetProperty(Credential.Password, value, Credential, (credential, password) => credential.Password = password);
    }

    /// <summary>
    ///     Список Scopes.
    /// </summary>
    public ObservableCollection<Uri> Scopes { get; } = [];

    /// <summary>
    ///     Список адресов подключения.
    /// </summary>
    public ObservableCollection<Uri> ConnectionUris { get; } = [];

    /// <summary>
    ///     Словарь токенов профилей и URI стримов.
    /// </summary>
    public ObservableDictionary<string, Uri> StreamUris { get; } = [];

    /// <summary>
    ///     Импортирует список адресов подключения.
    /// </summary>
    /// <param name="connectionUris">Список адресов подключения.</param>
    public void ImportConnectionUris(IEnumerable<Uri> connectionUris)
    {
        OnPropertyChanging(nameof(ConnectionUris));
        ConnectionUris.Clear();
        foreach (var connectionUri in connectionUris)
            ConnectionUris.Add(connectionUri);
        OnPropertyChanged(nameof(ConnectionUris));
    }

    /// <summary>
    ///     Пытается получить список URI стримов от ONVIF устройства.
    /// </summary>
    /// <param name="connectionUriIndex">Индекс элемента из списка адресов подключения.</param>
    /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp:// username:password@address").</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Возвращает <c>true</c> если удалось получить список URI стримов от ONVIF устройства, даже если он пуст; иначе <c>false</c>.</returns>
    public async Task<bool> TryReceivingStreamUrisAsync(int connectionUriIndex, bool addCredentialData, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ConnectionUris.IsValidIndex(connectionUriIndex))
                return false;
            OnPropertyChanging(nameof(StreamUris));
            StreamUris.Clear();
            await OnvifHelper.GetAllStreamUrisAsync(SessionFactory.CreateSession(ConnectionUris[connectionUriIndex]), addCredentialData, cancellationToken)
                .ForEachAsync(kvp => StreamUris.Add(kvp.Key, kvp.Value), cancellationToken).ConfigureAwait(false);
            OnPropertyChanged(nameof(StreamUris));
            return true;
        } catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту того же типа.
    /// </summary>
    /// <param name="other">Объект для сравнения.</param>
    /// <returns>Значение <c>true</c>, если текущий объект равен объекту <paramref name="other" />; иначе <c>false</c>.</returns>
    [SuppressMessage("ReSharper", "UsageOfDefaultStructEquality")]
    public bool Equals(IDiscoveredCamera? other) =>
        other != null &&
        (IpAddress?.Equals(other.IpAddress) ?? other.IpAddress is null) &&
        (MacAddress?.Equals(other.MacAddress) ?? other.MacAddress is null) &&
        (Title?.Equals(other.Title) ?? other.Title is null) &&
        Username.Equals(other.Username) &&
        Password.Equals(other.Password) &&
        (Manufacturer?.Equals(other.Manufacturer) ?? other.Manufacturer is null) &&
        (Model?.Equals(other.Model) ?? other.Model is null) &&
        Scopes.SequenceEqual(other.Scopes) &&
        ConnectionUris.SequenceEqual(other.ConnectionUris) &&
        StreamUris.SequenceEqual(other.StreamUris);

    /// <summary>
    ///     Импортирует список Scopes.
    /// </summary>
    /// <param name="scopes">Список Scopes.</param>
    public void ImportScopes(IEnumerable<Uri> scopes)
    {
        OnPropertyChanging(nameof(Scopes));
        Scopes.Clear();
        foreach (var scope in scopes)
            Scopes.Add(scope);
        OnPropertyChanged(nameof(Scopes));
    }

    /// <summary>
    ///     Импортирует список URI стримов.
    /// </summary>
    /// <param name="streamUris">Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</param>
    public void ImportStreamUris(IEnumerable<KeyValuePair<string, Uri>> streamUris)
    {
        OnPropertyChanging(nameof(StreamUris));
        StreamUris.Clear();
        foreach (var streamUri in streamUris)
            StreamUris.Add(streamUri.Key, streamUri.Value);
        OnPropertyChanged(nameof(StreamUris));
    }

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту.
    /// </summary>
    /// <param name="other">Объект для сравнения.</param>
    /// <returns>Значение <c>true</c>, если текущий объект равен объекту <paramref name="other" />; иначе <c>false</c>.</returns>
    public override bool Equals(object? other) => Equals(other as IDiscoveredCamera);

    /// <summary>
    ///     Возвращает хэш-код для текущего объекта.
    /// </summary>
    /// <returns>Хэш-код для текущего объекта.</returns>
    public override int GetHashCode() => HashCode.Combine(nameof(IDiscoveredCamera), Credential, SessionFactory, Scopes, ConnectionUris, StreamUris);
}