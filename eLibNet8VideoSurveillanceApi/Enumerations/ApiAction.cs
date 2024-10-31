using System.ComponentModel;

namespace eLibNet8VideoSurveillanceApi.Enumerations;

/// <summary>
///     Перечисление, представляющее различные действия API.
/// </summary>
public enum ApiAction
{
    #region Token

    /// <summary>
    ///     Получить конфигурацию токенов.
    /// </summary>
    [Description("gettokenconfig")]
    GetTokenConfig = 1001,

    /// <summary>
    ///     Получить токен пользователя.
    /// </summary>
    [Description("getutoken")]
    GetUserToken = 1002,

    /// <summary>
    ///     Получить токен устройства.
    /// </summary>
    [Description("getdtoken")]
    GetDeviceToken = 1003,

    /// <summary>
    ///     Получить общий токен.
    /// </summary>
    [Description("getctoken")]
    GetCommonToken = 1004,

    #endregion

    #region User

    /// <summary>
    ///     Получить данные пользователя.
    /// </summary>
    [Description("user/get-user")]
    GetUser = 2002,

    /// <summary>
    ///     Получить список пользователей.
    /// </summary>
    [Description("user/get-users")]
    GetUsers = 2001,

    /// <summary>
    ///     Зарегистрировать пользователя.
    /// </summary>
    [Description("user/register-user")]
    RegisterUser = 2003,

    /// <summary>
    ///     Редактировать пользователя.
    /// </summary>
    [Description("user/edit-user")]
    EditUser = 2004,

    /// <summary>
    ///     Изменить тариф пользователя.
    /// </summary>
    [Description("user/change-user-tariff")]
    ChangeUserTariff = 2005,

    /// <summary>
    ///     Удалить пользователя.
    /// </summary>
    [Description("user/delete-user")]
    DeleteUser = 2006,

    #endregion

    #region Device

    /// <summary>
    ///     Получить список устройств.
    /// </summary>
    [Description("device/get-devices")]
    GetDevices = 3001,

    /// <summary>
    ///     Получить данные устройства.
    /// </summary>
    [Description("device/get-device")]
    GetDevice = 3002,

    /// <summary>
    ///     Зарегистрировать устройство.
    /// </summary>
    [Description("device/register-device")]
    RegisterDevice = 3003,

    /// <summary>
    ///     Редактировать устройство.
    /// </summary>
    [Description("device/edit-device")]
    EditDevice = 3004,

    /// <summary>
    ///     Удалить устройство.
    /// </summary>
    [Description("device/delete-device")]
    DeleteDevice = 3005,

    /// <summary>
    ///     Привязать устройство.
    /// </summary>
    [Description("device/link-device")]
    LinkDevice = 3006,

    /// <summary>
    ///     Отвязать устройство.
    /// </summary>
    [Description("device/unlink-device")]
    UnlinkDevice = 3007,

    #endregion

    #region Camera

    /// <summary>
    ///     Получить список камер пользователя.
    /// </summary>
    [Description("camera/get-user-cameras")]
    GetUserCameras = 4001,

    /// <summary>
    ///     Получить список камер устройства.
    /// </summary>
    [Description("camera/get-device-cameras")]
    GetDeviceCameras = 4002,

    /// <summary>
    ///     Получить данные камеры.
    /// </summary>
    [Description("camera/get-camera")]
    GetCamera = 4003,

    /// <summary>
    ///     Зарегистрировать камеру.
    /// </summary>
    [Description("camera/register-camera")]
    RegisterCamera = 4004,

    /// <summary>
    ///     Редактировать камеру.
    /// </summary>
    [Description("camera/edit-camera")]
    EditCamera = 4005,

    /// <summary>
    ///     Удалить камеру.
    /// </summary>
    [Description("camera/delete-camera")]
    DeleteCamera = 4006,

    #endregion

    #region File

    /// <summary>
    ///     Получить список файлов пользователя.
    /// </summary>
    [Description("file/get-user-files")]
    GetUserFiles = 5001,

    /// <summary>
    ///     Получить список файлов устройства.
    /// </summary>
    [Description("file/get-device-files")]
    GetDeviceFiles = 5002,

    /// <summary>
    ///     Получить список файлов камеры.
    /// </summary>
    [Description("file/get-camera-files")]
    GetCameraFiles = 5003,

    /// <summary>
    ///     Получить данные файла.
    /// </summary>
    [Description("file/get-file")]
    GetFile = 5004,

    /// <summary>
    ///     Зарегистрировать файл.
    /// </summary>
    [Description("file/register-file")]
    RegisterFile = 5005,

    /// <summary>
    ///     Редактировать файл.
    /// </summary>
    [Description("file/edit-file")]
    EditFile = 5006,

    /// <summary>
    ///     Удалить файл.
    /// </summary>
    [Description("file/delete-file")]
    DeleteFile = 5007,

    /// <summary>
    ///     Загрузить файл.
    /// </summary>
    [Description("file/upload-file")]
    UploadFile = 5008,

    /// <summary>
    ///     Скачать файл (web версия).
    /// </summary>
    /// <remarks>Только для сайта или в крайних случаях.</remarks>
    [Description("file/web-download-file")]
    WebDownloadFile = 5009,

    /// <summary>
    ///     Скачать файл.
    /// </summary>
    [Description("file/download-file")]
    DownloadFile = 5010,

    /// <summary>
    ///     Удалить все файлы.
    /// </summary>
    [Description("file/delete-all-files")]
    DeleteAllFiles = 5011,

    /// <summary>
    ///     Быстрая загрузка файла.
    /// </summary>
    [Description("file/fast-upload-file")]
    FastUploadFile = 5012,

    #endregion

    #region OnlineStreamer

    /// <summary>
    ///     Получить список онлайн-стримов пользователя.
    /// </summary>
    [Description("online-streamer/get-user-online-streamers")]
    GetUserOnlineStreamers = 8001,

    /// <summary>
    ///     Получить данные онлайн-стрима.
    /// </summary>
    [Description("online-streamer/get-online-streamer")]
    GetOnlineStreamer = 8002,

    /// <summary>
    ///     Зарегистрировать онлайн-стрим.
    /// </summary>
    [Description("online-streamer/register-online-streamer")]
    RegisterOnlineStreamer = 8003,

    /// <summary>
    ///     Редактировать онлайн-стрим.
    /// </summary>
    [Description("online-streamer/edit-online-streamer")]
    EditOnlineStreamer = 8004,

    /// <summary>
    ///     Удалить онлайн-стрим.
    /// </summary>
    [Description("online-streamer/delete-online-streamer")]
    DeleteOnlineStreamer = 8005,

    #endregion

    /// <summary>
    ///     Нет действия.
    /// </summary>
    [Description("none")]
    None = 0
}