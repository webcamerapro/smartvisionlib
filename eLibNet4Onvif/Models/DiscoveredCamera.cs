using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using eLibNet4Core.Helpers;
using eLibNet4Core.Models;
using eLibNet4Onvif.Interfaces;
using JetBrains.Annotations;

namespace eLibNet4Onvif.Models
{
    /// <summary>
    ///     Класс, представляющий обнаруженную камеру.
    /// </summary>
    public class DiscoveredCamera : IDiscoveredCamera
    {
        private IPAddress _ipAddress;
        private MACAddress _macAddress;
        private string _manufacturer;
        private string _model;
        private string _password;
        private string _title;
        private string _username;

        /// <summary>
        ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
        /// </summary>
        /// <param name="ipAddress">IP-адрес камеры.</param>
        public DiscoveredCamera(IPAddress ipAddress) => _ipAddress = ipAddress;

        /// <summary>
        ///     Создаёт экземпляр класса <see cref="DiscoveredCamera" />.
        /// </summary>
        /// <param name="ipAddress">IP-адрес.</param>
        /// <param name="manufacturer">Производитель.</param>
        /// <param name="model">Модель.</param>
        /// <param name="scopes">Список Scopes.</param>
        /// <param name="connectionUris">Список адресов подключения.</param>
        public DiscoveredCamera(IPAddress ipAddress, string manufacturer, string model, IEnumerable<Uri> scopes, IEnumerable<Uri> connectionUris)
        {
            _ipAddress    = ipAddress;
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
            foreach (var connectionUri in connectionUris)
                ConnectionUris.Add(connectionUri);
            foreach (var streamUri in streamUris)
                StreamUris.Add(streamUri.Key, streamUri.Value);
        }

        /// <summary>
        ///     Событие, возникающее при изменении свойства.
        /// </summary>
        [CanBeNull]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     IP-адрес.
        /// </summary>
        [NotNull]
        public IPAddress IpAddress
        {
            get => _ipAddress;
            set
            {
                if (_ipAddress.Equals(value))
                    return;
                OnPropertyChanging();
                _ipAddress = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     MAC-адрес.
        /// </summary>
        [CanBeNull]
        public MACAddress MacAddress
        {
            get => _macAddress;
            set
            {
                if (_macAddress?.Equals(value) ?? value == null)
                    return;
                OnPropertyChanging();
                _macAddress = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Наименование.
        /// </summary>
        [CanBeNull]
        public string Title
        {
            get => _title;
            set
            {
                if (_title?.Equals(value) ?? value == null)
                    return;
                OnPropertyChanging();
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Имя пользователя.
        /// </summary>
        [CanBeNull]
        public string Username
        {
            get => _username;
            set
            {
                if (_username?.Equals(value) ?? value == null)
                    return;
                OnPropertyChanging();
                _username = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Пароль.
        /// </summary>
        [CanBeNull]
        public string Password
        {
            get => _password;
            set
            {
                if (_password?.Equals(value) ?? value == null)
                    return;
                OnPropertyChanging();
                _password = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Производитель.
        /// </summary>
        [CanBeNull]
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (_manufacturer?.Equals(value) ?? value == null)
                    return;
                OnPropertyChanging();
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Модель.
        /// </summary>
        [CanBeNull]
        public string Model
        {
            get => _model;
            set
            {
                if (_model?.Equals(value) ?? value == null)
                    return;
                OnPropertyChanging();
                _model = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Список Scopes.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ObservableCollection<Uri> Scopes { get; } = new ObservableCollection<Uri>();

        /// <summary>
        ///     Список адресов подключения.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ObservableCollection<Uri> ConnectionUris { get; } = new ObservableCollection<Uri>();

        /// <summary>
        ///     Словарь токенов профилей и URI стримов.
        /// </summary>
        [NotNull]
        public ObservableDictionary<string, Uri> StreamUris { get; } = new ObservableDictionary<string, Uri>();

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
        ///     Определяет, равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="other">Объект для сравнения.</param>
        /// <returns>Значение <c>true</c>, если текущий объект равен объекту <paramref name="other" />; иначе <c>false</c>.</returns>
        [SuppressMessage("ReSharper", "UsageOfDefaultStructEquality")]
        public bool Equals(IDiscoveredCamera other) =>
            other != null &&
            IpAddress.Equals(other.IpAddress) &&
            (MacAddress?.Equals(other.MacAddress) ?? other.MacAddress == null) &&
            (Title?.Equals(other.Title) ?? other.Title == null) &&
            (Username?.Equals(other.Username) ?? other.Username == null) &&
            (Password?.Equals(other.Password) ?? other.Password == null) &&
            (Manufacturer?.Equals(other.Manufacturer) ?? other.Manufacturer is null) &&
            (Model?.Equals(other.Model) ?? other.Model is null) &&
            Scopes.SequenceEqual(other.Scopes) &&
            ConnectionUris.SequenceEqual(other.ConnectionUris) &&
            StreamUris.SequenceEqual(other.StreamUris);

        /// <summary>
        ///     Событие, возникающее перед изменением свойства.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        ///     Определяет, равен ли текущий объект другому объекту.
        /// </summary>
        /// <param name="other">Объект для сравнения.</param>
        /// <returns>Значение <c>true</c>, если текущий объект равен объекту <paramref name="other" />; иначе <c>false</c>.</returns>
        public override bool Equals(object other) => Equals(other as IDiscoveredCamera);

        /// <summary>
        ///     Возвращает хэш-код для текущего объекта.
        /// </summary>
        /// <returns>Хэш-код для текущего объекта.</returns>
        public override int GetHashCode() => HashCodeHelper.Combine(nameof(IDiscoveredCamera), IpAddress, Scopes, ConnectionUris, StreamUris);

        private void OnPropertyChanging([CallerMemberName] string propertyName = null) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}