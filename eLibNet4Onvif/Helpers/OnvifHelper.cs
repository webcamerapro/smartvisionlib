using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.FSharp.Control;
using odm.core;
using onvif.services;
using utils;

namespace eLibNet4Onvif.Helpers
{
    /// <summary>
    ///     Вспомогательный класс для работы с ONVIF.
    /// </summary>
    public static class OnvifHelper
    {
        /// <summary>
        ///     Создаёт сессию NVT.
        /// </summary>
        /// <param name="uri">URI устройства.</param>
        /// <param name="networkCredential">Учетные данные сети.</param>
        /// <returns>Сессия NVT.</returns>
        public static INvtSession CreateNvtSession(Uri uri, NetworkCredential networkCredential) => new NvtSessionFactory(networkCredential).CreateSession(uri);

        /// <summary>
        ///     Создаёт сессию NVT.
        /// </summary>
        /// <param name="uri">URI устройства.</param>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Сессия NVT.</returns>
        public static INvtSession CreateNvtSession(Uri uri, string username, string password) => CreateNvtSession(uri, new NetworkCredential { UserName = username, Password = password });

        /// <summary>
        ///     Изменяет пароль пользователя.
        /// </summary>
        /// <param name="nvtSession">Сессия NVT.</param>
        /// <param name="newPassword">Новый пароль.</param>
        public static void ChangePassword(INvtSession nvtSession, string newPassword)
        {
            foreach (var user in nvtSession.GetUsers().RunSynchronously())
            {
                if (user.username != nvtSession.credentials.UserName)
                    continue;
                user.password = newPassword;
                nvtSession.SetUser(new[] { user }).RunSynchronously();
                nvtSession.credentials.Password = newPassword;
                return;
            }
        }

        /// <summary>
        ///     Асинхронно изменяет пароль пользователя.
        /// </summary>
        /// <param name="nvtSession">Сессия NVT.</param>
        /// <param name="newPassword">Новый пароль.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public static async Task ChangePasswordAsync(INvtSession nvtSession, string newPassword, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            foreach (var user in await FSharpAsync.StartAsTask(nvtSession.GetUsers(), null, cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (user.username != nvtSession.credentials.UserName)
                    continue;
                user.password = newPassword;
                await FSharpAsync.StartAsTask(nvtSession.SetUser(new[] { user }), null, cancellationToken);
                nvtSession.credentials.Password = newPassword;
                return;
            }
        }

        /// <summary>
        ///     Получает URI потока.
        /// </summary>
        /// <param name="nvtSession">Сессия NVT.</param>
        /// <param name="profileToken">Токен профиля.</param>
        /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp://username:password@address").</param>
        /// <returns>URI потока, если найден; иначе <c>null</c>.</returns>
        public static Uri GetStreamUri(INvtSession nvtSession, string profileToken, bool addCredentialData = false) =>
            Uri.TryCreate(addCredentialData
                ? nvtSession.GetStreamUri(new StreamSetup { transport = new Transport { protocol = TransportProtocol.rtsp } }, profileToken).RunSynchronously().uri
                    .Replace("://", "://" + nvtSession.credentials.UserName + ":" + nvtSession.credentials.Password + "@")
                : nvtSession.GetStreamUri(new StreamSetup { transport = new Transport { protocol = TransportProtocol.rtsp } }, profileToken).RunSynchronously().uri, UriKind.Absolute, out var streamUri)
                ? streamUri
                : null;

        /// <summary>
        ///     Асинхронно получает URI потока.
        /// </summary>
        /// <param name="nvtSession">Сессия NVT.</param>
        /// <param name="profileToken">Токен профиля.</param>
        /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp://username:password@address").</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>URI потока, если найден; иначе <c>null</c>.</returns>
        public static async Task<Uri> GetStreamUriAsync(INvtSession nvtSession, string profileToken, bool addCredentialData = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Uri.TryCreate(addCredentialData
                ? (await FSharpAsync.StartAsTask(nvtSession.GetStreamUri(new StreamSetup { transport = new Transport { protocol = TransportProtocol.rtsp } }, profileToken), null, cancellationToken)).uri.Replace("://",
                    "://" + nvtSession.credentials.UserName + ":" + nvtSession.credentials.Password + "@")
                : (await FSharpAsync.StartAsTask(nvtSession.GetStreamUri(new StreamSetup { transport = new Transport { protocol = TransportProtocol.rtsp } }, profileToken), null, cancellationToken)).uri, UriKind.Absolute, out var streamUri)
                ? streamUri
                : null;
        }

        /// <summary>
        ///     Получает все профили и их URI потока.
        /// </summary>
        /// <param name="nvtSession">Сессия NVT.</param>
        /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp://username:password@address").</param>
        /// <returns>Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</returns>
        public static IEnumerable<KeyValuePair<string, Uri>> GetAllStreamUris(INvtSession nvtSession, bool addCredentialData = false) =>
            nvtSession.GetProfiles().RunSynchronously().Select(profile => new KeyValuePair<string, Uri>(profile.token, GetStreamUri(nvtSession, profile.token, addCredentialData))).Where(kvp => kvp.Value != null);

        /// <summary>
        ///     Асинхронно получает все профили и их URI потока.
        /// </summary>
        /// <param name="nvtSession">Сессия NVT.</param>
        /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp://username:password@address").</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</returns>
        public static async Task<IEnumerable<KeyValuePair<string, Uri>>> GetAllStreamUrisAsync(INvtSession nvtSession, bool addCredentialData = false, CancellationToken cancellationToken = default)
        {
            var streamUris = new List<KeyValuePair<string, Uri>>();
            foreach (var profile in await FSharpAsync.StartAsTask(nvtSession.GetProfiles(), null, cancellationToken))
            {
                var uri = await GetStreamUriAsync(nvtSession, profile.token, addCredentialData, cancellationToken);
                if (uri != null)
                    streamUris.Add(new KeyValuePair<string, Uri>(profile.token, uri));
            }

            return streamUris;
        }
    }
}