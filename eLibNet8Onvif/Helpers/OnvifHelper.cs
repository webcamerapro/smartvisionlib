using System.Net;
using System.Threading.Channels;
using Microsoft.FSharp.Control;
using odm.core;
using onvif.services;
using utils;

namespace eLibNet8Onvif.Helpers;

/// <summary>
///     Вспомогательный класс для работы с ONVIF.
/// </summary>
public static class OnvifHelper
{
    /// <summary>
    ///     Создаёт сессию NVT. Создает новый менеджер сессий <see cref="NvtSessionFactory" />. Рекомендуется использовать для одиночного подключения, когда менеджер сессий заранее не определен.
    ///     В иных случаях используйте уже существующий менеджер сессий:
    ///     <code>
    ///         var nvtSessionFactory = new NvtSessionFactory(new NetworkCredential {"username", "password"});
    ///         var nvtSession = nvtSessionFactory.CreateSession("uri");
    ///     </code>
    /// </summary>
    /// <param name="networkCredential">Учетные данные сети.</param>
    /// <param name="uri">URI устройства.</param>
    /// <returns>Сессия NVT.</returns>
    public static INvtSession CreateSession(NetworkCredential networkCredential, Uri uri) => new NvtSessionFactory(networkCredential).CreateSession(uri);

    /// <summary>
    ///     Создаёт сессию NVT. Создает новый менеджер сессий <see cref="NvtSessionFactory" />. Рекомендуется использовать для одиночного подключения, когда менеджер сессий заранее не определен.
    ///     В иных случаях используйте уже существующий менеджер сессий:
    ///     <code>
    ///         var nvtSessionFactory = new NvtSessionFactory(new NetworkCredential {"username", "password"});
    ///         var nvtSession = nvtSessionFactory.CreateSession("uri");
    ///     </code>
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль.</param>
    /// <param name="uri">URI устройства.</param>
    /// <returns>Сессия NVT.</returns>
    public static INvtSession CreateSession(string username, string password, Uri uri) => CreateSession(new() { UserName = username, Password = password }, uri);

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
            nvtSession.SetUser([user]).RunSynchronously();
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
        foreach (var user in await FSharpAsync.StartAsTask(nvtSession.GetUsers(), null, cancellationToken).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user.username != nvtSession.credentials.UserName)
                continue;
            user.password = newPassword;
            await FSharpAsync.StartAsTask(nvtSession.SetUser([user]), null, cancellationToken).ConfigureAwait(false);
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
    public static Uri? GetStreamUri(INvtSession nvtSession, string profileToken, bool addCredentialData = false) => Uri.TryCreate(addCredentialData
        ? nvtSession.GetStreamUri(new() { transport = new() { protocol = TransportProtocol.rtsp } }, profileToken).RunSynchronously().uri
            .Replace("://", "://" + nvtSession.credentials.UserName + ":" + nvtSession.credentials.Password + "@")
        : nvtSession.GetStreamUri(new() { transport = new() { protocol = TransportProtocol.rtsp } }, profileToken).RunSynchronously().uri, UriKind.Absolute, out var streamUri)
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
    public static async Task<Uri?> GetStreamUriAsync(INvtSession nvtSession, string profileToken, bool addCredentialData = false, CancellationToken cancellationToken = default) => Uri.TryCreate(addCredentialData
        ? (await FSharpAsync.StartAsTask(nvtSession.GetStreamUri(new() { transport = new() { protocol = TransportProtocol.rtsp } }, profileToken), null, cancellationToken).ConfigureAwait(false)).uri.Replace("://",
            "://" + nvtSession.credentials.UserName + ":" + nvtSession.credentials.Password + "@")
        : (await FSharpAsync.StartAsTask(nvtSession.GetStreamUri(new() { transport = new() { protocol = TransportProtocol.rtsp } }, profileToken), null, cancellationToken).ConfigureAwait(false)).uri, UriKind.Absolute, out var streamUri)
        ? streamUri
        : null;

    /// <summary>
    ///     Асинхронно получает все профили и их URI потока.
    /// </summary>
    /// <param name="nvtSession">Сессия NVT.</param>
    /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp://username:password@address").</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронный перечислитель <see cref="KeyValuePair{TKey,TValue}" /> из токена профиля и URI потока.</returns>
    public static IAsyncEnumerable<KeyValuePair<string, Uri>> GetAllStreamUrisAsync(INvtSession nvtSession, bool addCredentialData = false, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var channel = Channel.CreateUnbounded<KeyValuePair<string, Uri>>();
        _ = ReceivingStreamUrisAsync(channel.Writer, nvtSession, addCredentialData, cancellationToken);
        return channel.Reader.ReadAllAsync(cancellationToken);
    }

    /// <summary>
    ///     Асинхронно получает все профили и их URI потока с использованием указанного ChannelWriter.
    /// </summary>
    /// <param name="channelWriter">ChannelWriter для записи найденных профилей и их URI потока.</param>
    /// <param name="nvtSession">Сессия NVT.</param>
    /// <param name="addCredentialData">Определяет добавлять ли имя пользователя и пароль перед адресом ("rtsp://username:password@address").</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public static async Task GetAllStreamUrisAsync(ChannelWriter<KeyValuePair<string, Uri>> channelWriter, INvtSession nvtSession, bool addCredentialData = false, CancellationToken cancellationToken = default) =>
        await ReceivingStreamUrisAsync(channelWriter, nvtSession, addCredentialData, cancellationToken).ConfigureAwait(false);

    private static async Task ReceivingStreamUrisAsync(ChannelWriter<KeyValuePair<string, Uri>> channelWriter, INvtSession nvtSession, bool addCredentialData, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            foreach (var profile in await FSharpAsync.StartAsTask(nvtSession.GetProfiles(), null, cancellationToken).ConfigureAwait(false))
                if (await GetStreamUriAsync(nvtSession, profile.token, addCredentialData, cancellationToken).ConfigureAwait(false) is { } uri)
                    await channelWriter.WriteAsync(new(profile.token, uri), cancellationToken).ConfigureAwait(false);
            channelWriter.TryComplete();
        } catch (Exception e) when (e is OperationCanceledException)
        {
            channelWriter.TryComplete();
        } catch (Exception e)
        {
            channelWriter.TryComplete(e);
            throw;
        }
    }
}