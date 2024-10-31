using System.ComponentModel;

namespace eLibNet8VideoSurveillanceApi.Enumerations;

/// <summary>
///     Перечисление, представляющее различные сообщения API.
/// </summary>
public enum ApiMessage
{
    #region Сообщения об успешном выполнении

    /// <summary>
    ///     Конфигурация токенов получена.
    /// </summary>
    [Description("token config has been received.")]
    TokenConfigRecieved = 1101,

    /// <summary>
    ///     Токен пользователя получен.
    /// </summary>
    [Description("user token has been received.")]
    UserTokenRecieved = 1102,

    /// <summary>
    ///     Токен устройства получен.
    /// </summary>
    [Description("device token has been received.")]
    DeviceTokenRecieved = 1103,

    /// <summary>
    ///     Общий токен получен.
    /// </summary>
    [Description("common token has been received.")]
    CommonTokenRecieved = 1104,

    /// <summary>
    ///     Список пользователей успешно получен.
    /// </summary>
    [Description("user list successfully loaded.")]
    UserListReceived = 2101,

    /// <summary>
    ///     Данные пользователя успешно получены.
    /// </summary>
    [Description("user data successfully loaded.")]
    UserDataReceived = 2102,

    /// <summary>
    ///     Пользователь успешно зарегистрирован.
    /// </summary>
    [Description("user successfully registered.")]
    UserRegistered = 2103,

    /// <summary>
    ///     Пользователь успешно отредактирован.
    /// </summary>
    [Description("user successfully edited.")]
    UserEdited = 2104,

    /// <summary>
    ///     Тариф пользователя успешно изменен.
    /// </summary>
    [Description("user tariff successfully changed.")]
    UserTariffChanged = 2105,

    /// <summary>
    ///     Пользователь успешно удален.
    /// </summary>
    [Description("user successfully deleted.")]
    UserDeleted = 2106,

    /// <summary>
    ///     Список устройств успешно получен.
    /// </summary>
    [Description("device list successfully loaded.")]
    DeviceListReceived = 3101,

    /// <summary>
    ///     Данные устройства успешно получены.
    /// </summary>
    [Description("device data successfully loaded.")]
    DeviceDataReceived = 3102,

    /// <summary>
    ///     Устройство успешно зарегистрировано.
    /// </summary>
    [Description("device successfully registered.")]
    DeviceRegistered = 3103,

    /// <summary>
    ///     Устройство успешно отредактировано.
    /// </summary>
    [Description("device successfully edited.")]
    DeviceEdited = 3104,

    /// <summary>
    ///     Устройство успешно удалено.
    /// </summary>
    [Description("device successfully deleted.")]
    DeviceDeleted = 3105,

    /// <summary>
    ///     Устройство успешно привязано.
    /// </summary>
    [Description("device successfully linked.")]
    DeviceLinked = 3106,

    /// <summary>
    ///     Устройство успешно отвязано.
    /// </summary>
    [Description("device successfully unlinked.")]
    DeviceUnlinked = 3107,

    /// <summary>
    ///     Список камер успешно получен.
    /// </summary>
    [Description("camera list successfully loaded.")]
    CameraListReceived = 4101,

    /// <summary>
    ///     Данные камеры успешно получены.
    /// </summary>
    [Description("camera data successfully loaded.")]
    CameraDataReceived = 4102,

    /// <summary>
    ///     Камера успешно зарегистрирована.
    /// </summary>
    [Description("camera successfully registered.")]
    CameraRegistered = 4103,

    /// <summary>
    ///     Камера успешно отредактирована.
    /// </summary>
    [Description("camera successfully edited.")]
    CameraEdited = 4104,

    /// <summary>
    ///     Камера успешно удалена.
    /// </summary>
    [Description("camera successfully deleted.")]
    CameraDeleted = 4105,

    /// <summary>
    ///     Список файлов успешно получен.
    /// </summary>
    [Description("file list successfully loaded.")]
    FileListReceived = 5101,

    /// <summary>
    ///     Данные файла успешно получены.
    /// </summary>
    [Description("file data successfully loaded.")]
    FileDataReceived = 5102,

    /// <summary>
    ///     Файл успешно зарегистрирован.
    /// </summary>
    [Description("file successfully registered.")]
    FileRegistered = 5103,

    /// <summary>
    ///     Файл успешно отредактирован.
    /// </summary>
    [Description("file successfully edited.")]
    FileEdited = 5104,

    /// <summary>
    ///     Файл успешно удален.
    /// </summary>
    [Description("file successfully deleted.")]
    FileDeleted = 5105,

    /// <summary>
    ///     Файл успешно загружен.
    /// </summary>
    [Description("file successfully uploaded.")]
    FileUploaded = 5106,

    /// <summary>
    ///     Все файлы успешно удалены.
    /// </summary>
    [Description("all files successfully deleted.")]
    FilesDeleted = 5107,

    /// <summary>
    ///     Список онлайн-стримов успешно получен.
    /// </summary>
    [Description("online streamer list successfully loaded.")]
    OnlineStreamerListReceived = 8101,

    /// <summary>
    ///     Данные онлайн-стрима успешно получены.
    /// </summary>
    [Description("online streamer data successfully loaded.")]
    OnlineStreamerDataReceived = 8102,

    /// <summary>
    ///     Онлайн-стрим успешно зарегистрирован.
    /// </summary>
    [Description("online streamer successfully registered.")]
    OnlineStreamerRegistered = 8103,

    /// <summary>
    ///     Онлайн-стрим успешно отредактирован.
    /// </summary>
    [Description("online streamer successfully edited.")]
    OnlineStreamerEdited = 8104,

    /// <summary>
    ///     Онлайн-стрим успешно удален.
    /// </summary>
    [Description("online streamer successfully deleted.")]
    OnlineStreamerDeleted = 8105,

    #endregion

    #region Сообщения об ошибках

    /// <summary>
    ///     Неправильный формат электронной почты.
    /// </summary>
    [Description("wrong email format.")]
    UserEmailWrongFormat = 1201,

    /// <summary>
    ///     Пользователь не существует.
    /// </summary>
    [Description("user does not exist.")]
    UserDoesNotExist = 1202,

    /// <summary>
    ///     Неправильный адрес электронной почты или пароль.
    /// </summary>
    [Description("user email or password is wrong.")]
    UserEmailOrPasswordIsWrong = 1203,

    /// <summary>
    ///     Ошибка генерации токена пользователя.
    /// </summary>
    [Description("user token generation failed.")]
    UserTokenFailed = 1204,

    /// <summary>
    ///     Неправильный GUID устройства.
    /// </summary>
    [Description("device guid is wrong.")]
    DeviceGuidIsWrong = 1205,

    /// <summary>
    ///     Неправильный код устройства.
    /// </summary>
    [Description("device code is wrong.")]
    DeviceCodeIsWrong = 1206,

    /// <summary>
    ///     Ошибка генерации токена устройства.
    /// </summary>
    [Description("device token generation failed.")]
    DeviceTokenFailed = 1207,

    /// <summary>
    ///     Ошибка генерации общего токена.
    /// </summary>
    [Description("common token generation failed.")]
    CommonTokenFailed = 1208,

    /// <summary>
    ///     Ошибка получения конфигурации токенов.
    /// </summary>
    [Description("getting token config failed.")]
    GettingTokenConfigFailed = 1209,

    /// <summary>
    ///     Параметр 'uToken' обязателен.
    /// </summary>
    [Description("input parameter 'uToken' is required.")]
    UserTokenEmpty = 2201,

    /// <summary>
    ///     Неправильный токен пользователя.
    /// </summary>
    [Description("invalid user token.")]
    UserTokenInvalid = 2202,

    /// <summary>
    ///     Пользователь с таким токеном не существует.
    /// </summary>
    [Description("user for this token does not exist.")]
    UserNotExist = 2203,

    /// <summary>
    ///     Пользователь с таким ID не существует.
    /// </summary>
    [Description("user for this id does not exist.")]
    UserIdNotExist = 2204,

    /// <summary>
    ///     Пользователь с такими данными уже существует.
    /// </summary>
    [Description("user for this data already exist.")]
    UserDataExist = 2205,

    /// <summary>
    ///     У пользователя нет тарифного плана.
    /// </summary>
    [Description("user has no tariff plan.")]
    UserHasNoTariff = 2206,

    /// <summary>
    ///     У пользователя нет устройств.
    /// </summary>
    [Description("user has no devices.")]
    UserHasNoDevices = 2207,

    /// <summary>
    ///     У пользователя нет камер.
    /// </summary>
    [Description("user has no cameras.")]
    UserHasNoCameras = 2208,

    /// <summary>
    ///     Параметр 'email' обязателен.
    /// </summary>
    [Description("input parameter 'email' is required.")]
    UserEmailEmpty = 2209,

    /// <summary>
    ///     Параметр 'password' обязателен.
    /// </summary>
    [Description("input parameter 'password' is required.")]
    UserPasswordEmpty = 2210,

    /// <summary>
    ///     Пользователь уже имеет этот тариф.
    /// </summary>
    [Description("user already have this tariff.")]
    UserAlreadyHaveThisTariff = 2211,

    /// <summary>
    ///     Параметр 'dToken' обязателен.
    /// </summary>
    [Description("input parameter 'dToken' is required.")]
    DeviceTokenEmpty = 3201,

    /// <summary>
    ///     Неправильный токен устройства.
    /// </summary>
    [Description("invalid device token.")]
    DeviceTokenInvalid = 3202,

    /// <summary>
    ///     Устройство с таким токеном не существует.
    /// </summary>
    [Description("device for this token does not exist.")]
    DeviceNotExist = 3203,

    /// <summary>
    ///     Устройство с таким ID не существует.
    /// </summary>
    [Description("device for this id does not exist.")]
    DeviceIdNotExist = 3204,

    /// <summary>
    ///     Устройство с такими данными уже существует.
    /// </summary>
    [Description("device for this data already exist.")]
    DeviceDataExist = 3205,

    /// <summary>
    ///     Устройство уже привязано к пользователю с таким токеном.
    /// </summary>
    [Description("device for this token already linked.")]
    DeviceAlreadyLinked = 3206,

    /// <summary>
    ///     Устройство уже отвязано от пользователя с таким токеном.
    /// </summary>
    [Description("device for this token already unlinked.")]
    DeviceAlreadyUnlinked = 3207,

    /// <summary>
    ///     Один из параметров 'dToken' или 'id' обязателен.
    /// </summary>
    [Description("one input parameter 'dToken' or 'id' is required.")]
    DeviceTokenOrIdEmpty = 3208,

    /// <summary>
    ///     Камера с таким ID не существует.
    /// </summary>
    [Description("camera for this id does not exist.")]
    CameraIdNotExist = 4201,

    /// <summary>
    ///     Камера с такими данными уже существует.
    /// </summary>
    [Description("camera for this data already exist.")]
    CameraDataExist = 4202,

    /// <summary>
    ///     Параметр 'id_camera' обязателен.
    /// </summary>
    [Description("input parameter 'id_camera' is required.")]
    CameraIdCameraEmpty = 4203,

    /// <summary>
    ///     Параметр 'device_id' не используется для этого действия. Вместо этого должны быть указаны параметры 'dToken' или 'id'.
    /// </summary>
    [Description("input parameter 'device_id' is not used for this action. Parameters 'dToken' or 'id' should be specified instead.")]
    CameraNoNeedDeviceId = 4204,

    /// <summary>
    ///     Файл с таким ID не существует.
    /// </summary>
    [Description("file for this id does not exist.")]
    FileIdNotExist = 5201,

    /// <summary>
    ///     Файл с такими данными уже существует.
    /// </summary>
    [Description("file for this data already exist.")]
    FileDataExist = 5202,

    /// <summary>
    ///     Превышен лимит загрузки данных для этого тарифа.
    /// </summary>
    [Description("data upload limit exceeded for this tariff.")]
    FileUploadLimitExceeded = 5203,

    /// <summary>
    ///     Параметр 'title' обязателен.
    /// </summary>
    [Description("input parameter 'title' is required.")]
    FileTitleEmpty = 5204,

    /// <summary>
    ///     Параметр 'file_name' обязателен.
    /// </summary>
    [Description("input parameter 'file_name' is required.")]
    FileFileNameEmpty = 5205,

    /// <summary>
    ///     Параметр 'size' обязателен.
    /// </summary>
    [Description("input parameter 'size' is required.")]
    FileSizeEmpty = 5206,

    /// <summary>
    ///     Ошибка копирования временных данных файла.
    /// </summary>
    [Description("failed to copy file temp data.")]
    FileTempDataFailed = 5207,

    /// <summary>
    ///     Файл уже существует.
    /// </summary>
    [Description("file already exist.")]
    FileAlreadyExist = 5208,

    /// <summary>
    ///     Файл не может быть удален.
    /// </summary>
    [Description("file cannot be deleted.")]
    FileNotDeleted = 5209,

    /// <summary>
    ///     Ошибка преобразования файла или создания превью изображения.
    /// </summary>
    [Description("failed to convert file or create preview image.")]
    FileNotUploaded = 5210,

    /// <summary>
    ///     Ошибка загрузки файла.
    /// </summary>
    [Description("failed to downloading file.")]
    FileNotDownloaded = 5211,

    /// <summary>
    ///     Размер файла не совпадает.
    /// </summary>
    [Description("file size does not match.")]
    FileSizeNotMatch = 5212,

    /// <summary>
    ///     Имя файла не совпадает.
    /// </summary>
    [Description("file name does not match.")]
    FileNameNotMatch = 5213,

    /// <summary>
    ///     Тариф указанного пользователя не существует.
    /// </summary>
    [Description("tariff for this user does not exist.")]
    TariffUserNotExist = 6201,

    /// <summary>
    ///     Тариф для этого ID не существует.
    /// </summary>
    [Description("tariff for this id does not exist.")]
    TariffIdNotExist = 6202,

    /// <summary>
    ///     Параметр 'tariffId' обязателен.
    /// </summary>
    [Description("input parameter 'tariffId' is required.")]
    TariffIdEmpty = 6203,

    /// <summary>
    ///     Параметры обязательны.
    /// </summary>
    [Description("input parameters is required.")]
    ParametersEmpty = 7201,

    /// <summary>
    ///     Неправильный метод запроса.
    /// </summary>
    [Description("wrong request method.")]
    WrongRequestMethod = 7202,

    /// <summary>
    ///     Нет разрешения на выполнение этого запроса.
    /// </summary>
    [Description("don't have permission to execute this query.")]
    DontHavePermission = 7203,

    /// <summary>
    ///     Параметр 'id' обязателен.
    /// </summary>
    [Description("input parameter 'id' is required.")]
    IdEmpty = 7204,

    /// <summary>
    ///     Неизвестное или неизменяемое свойство.
    /// </summary>
    [Description("unknown or non-editable property.")]
    PropertyUnknown = 7205,

    /// <summary>
    ///     Нет разрешения на изменение свойства.
    /// </summary>
    [Description("don't have permission to change property.")]
    PropertyPermission = 7206,

    /// <summary>
    ///     Свойство не может быть пустым.
    /// </summary>
    [Description("property cannot be empty.")]
    PropertyEmpty = 7207,

    /// <summary>
    ///     Онлайн-стрим для этого ID не существует.
    /// </summary>
    [Description("online streamer for this id does not exist.")]
    OnlineStreamerIdNotExist = 8201,

    /// <summary>
    ///     Онлайн-стрим с такими данными уже существует.
    /// </summary>
    [Description("online streamer for this data already exist.")]
    OnlineStreamerDataExist = 8202,

    /// <summary>
    ///     Параметр 'camera_id' обязателен.
    /// </summary>
    [Description("input parameter 'camera_id' is required.")]
    OnlineStreamerCameraIdEmpty = 8203,

    /// <summary>
    ///     Параметр 'rtmp_address' обязателен.
    /// </summary>
    [Description("input parameter 'rtmp_address' is required.")]
    OnlineStreamerRtmpAddressEmpty = 8204,

    /// <summary>
    ///     Параметр 'translation_key' обязателен.
    /// </summary>
    [Description("input parameter 'translation_key' is required.")]
    OnlineStreamerTranslationKeyEmpty = 8205,

    /// <summary>
    ///     Онлайн-стрим не может быть удален.
    /// </summary>
    [Description("online streamer cannot be deleted.")]
    OnlineStreamerNotDeleted = 8206,

    #endregion

    /// <summary>
    ///     Нет сообщения.
    /// </summary>
    [Description("none.")]
    None = 0
}