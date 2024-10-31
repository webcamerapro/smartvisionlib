using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using eLibNet8Core.Models;
using eLibNet8Onvif.Interfaces;

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
    private IPAddress _ipAddress;

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
    ///     Пароль.
    /// </summary>
    [ObservableProperty]
    private string? _password;

    /// <summary>
    ///     Наименование.
    /// </summary>
    [ObservableProperty]
    private string? _title;

    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    [ObservableProperty]
    private string? _username;

    /// <summary>
    ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    public DiscoveredCamera(IPAddress ipAddress) => _ipAddress = ipAddress;

    /// <summary>
    ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
    /// </summary>
    /// <param name="ipAddress">IP-адрес.</param>
    /// <param name="manufacturer">Производитель.</param>
    /// <param name="model">Модель.</param>
    /// <param name="scopes">Список Scopes.</param>
    /// <param name="types">Список типов.</param>
    /// <param name="xAddresses">Список адресов подключения.</param>
    public DiscoveredCamera(IPAddress ipAddress, string manufacturer, string model, IEnumerable<string> scopes, IEnumerable<string> types, IEnumerable<string> xAddresses)
    {
        _ipAddress    = ipAddress;
        _manufacturer = manufacturer;
        _model        = model;
        foreach (var scope in scopes)
            Scopes.Add(scope);
        foreach (var type in types)
            Types.Add(type);
        foreach (var xAddress in xAddresses)
            XAddresses.Add(xAddress);
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
    /// <param name="types">Список типов.</param>
    /// <param name="xAddresses">Список адресов подключения.</param>
    /// <param name="streamUris">Словарь токенов профилей и URI стримов.</param>
    public DiscoveredCamera(IPAddress ipAddress, MACAddress macAddress, string title, string username, string password, string manufacturer, string model, IEnumerable<string> scopes, IEnumerable<string> types, IEnumerable<string> xAddresses,
        IEnumerable<KeyValuePair<string, Uri>> streamUris)
    {
        _ipAddress    = ipAddress;
        _macAddress   = macAddress;
        _title        = title;
        _username     = username;
        _password     = password;
        _manufacturer = manufacturer;
        _model        = model;
        foreach (var scope in scopes)
            Scopes.Add(scope);
        foreach (var type in types)
            Types.Add(type);
        foreach (var xAddress in xAddresses)
            XAddresses.Add(xAddress);
        foreach (var streamUri in streamUris)
            StreamUris.Add(streamUri.Key, streamUri.Value);
    }

    /// <summary>
    ///     Список Scopes.
    /// </summary>
    public ObservableCollection<string> Scopes { get; } = [];

    /// <summary>
    ///     Список типов.
    /// </summary>
    public ObservableCollection<string> Types { get; } = [];

    /// <summary>
    ///     Список адресов подключения.
    /// </summary>
    public ObservableCollection<string> XAddresses { get; } = [];

    /// <summary>
    ///     Словарь токенов профилей и URI стримов.
    /// </summary>
    public ObservableDictionary<string, Uri> StreamUris { get; } = [];

    /// <summary>
    ///     Импортирует список адресов подключения.
    /// </summary>
    /// <param name="xAddresses">Список адресов подключения.</param>
    public void ImportXAddresses(IEnumerable<string> xAddresses)
    {
        OnPropertyChanging(nameof(XAddresses));
        XAddresses.Clear();
        foreach (var xAddress in xAddresses)
            XAddresses.Add(xAddress);
        OnPropertyChanged(nameof(XAddresses));
    }

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту того же типа.
    /// </summary>
    /// <param name="other">Объект для сравнения.</param>
    /// <returns>Значение <c>true</c>, если текущий объект равен объекту <paramref name="other" />; иначе <c>false</c>.</returns>
    [SuppressMessage("ReSharper", "UsageOfDefaultStructEquality")]
    public virtual bool Equals(IDiscoveredCamera? other) =>
        other != null &&
        IpAddress.Equals(other.IpAddress) &&
        (MacAddress?.Equals(other.MacAddress) ?? other.MacAddress is null) &&
        (Title?.Equals(other.Title) ?? other.Title is null) &&
        (Username?.Equals(other.Username) ?? other.Username is null) &&
        (Password?.Equals(other.Password) ?? other.Password is null) &&
        (Manufacturer?.Equals(other.Manufacturer) ?? other.Manufacturer is null) &&
        (Model?.Equals(other.Model) ?? other.Model is null) &&
        Scopes.SequenceEqual(other.Scopes) &&
        Types.SequenceEqual(other.Types) &&
        XAddresses.SequenceEqual(other.XAddresses) &&
        StreamUris.SequenceEqual(other.StreamUris);

    /// <summary>
    ///     Импортирует список Scopes.
    /// </summary>
    /// <param name="scopes">Список Scopes.</param>
    public void ImportScopes(IEnumerable<string> scopes)
    {
        OnPropertyChanging(nameof(Scopes));
        Scopes.Clear();
        foreach (var scope in scopes)
            Types.Add(scope);
        OnPropertyChanged(nameof(Scopes));
    }

    /// <summary>
    ///     Импортирует список типов.
    /// </summary>
    /// <param name="types">Список типов.</param>
    public void ImportTypes(IEnumerable<string> types)
    {
        OnPropertyChanging(nameof(Types));
        Types.Clear();
        foreach (var type in types)
            Types.Add(type);
        OnPropertyChanged(nameof(Types));
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
    ///     Импортирует список URI стримов.
    /// </summary>
    /// <param name="streamUris">Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</param>
    public void ImportStreamUris(IEnumerable<KeyValuePair<string, string>> streamUris)
    {
        OnPropertyChanging(nameof(StreamUris));
        StreamUris.Clear();
        foreach (var streamUri in streamUris)
            if (Uri.TryCreate(streamUri.Value, UriKind.Absolute, out var uri))
                StreamUris.Add(streamUri.Key, uri);
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
    public override int GetHashCode() => HashCode.Combine(nameof(IDiscoveredCamera), IpAddress, Scopes, Types, XAddresses, StreamUris);
}