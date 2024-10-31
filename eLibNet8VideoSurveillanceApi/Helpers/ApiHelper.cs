using System.Diagnostics.CodeAnalysis;
using System.Text;
using eLibNet8Core.Extensions;
using eLibNet8VideoSurveillanceApi.Enumerations;
using Newtonsoft.Json.Linq;

namespace eLibNet8VideoSurveillanceApi.Helpers;

/// <summary>
///     Класс, предоставляющий вспомогательные методы для работы с API.
/// </summary>
public static class ApiHelper
{
    /// <summary>
    ///     Проверяет, является ли ответ допустимым.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <returns>Возвращает <c>true</c>, если ответ допустим; иначе <c>false</c>.</returns>
    public static bool IsValidResponse([NotNullWhen(true)] JObject? response) => response is not null && response.ContainsKey("action") && response.ContainsKey("result") && response.ContainsKey("message") &&
                                                                                 response.ContainsKey("actionString") && response.ContainsKey("resultString") && response.ContainsKey("messageString") && response.ContainsKey("dateTime") &&
                                                                                 (response.ContainsKey("token") || response.ContainsKey("data") || response.ContainsKey("list"));

    /// <summary>
    ///     Проверяет, является ли ответ допустимым, и определяет тип ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="responseType">Тип ответа <see cref="ApiResponseType" />.</param>
    /// <returns>Возвращает <c>true</c>, если ответ допустим; иначе <c>false</c>.</returns>
    public static bool IsValidResponse([NotNullWhen(true)] JObject? response, out ApiResponseType responseType) => (responseType = GetResponseType(response)) != ApiResponseType.Undefined && response is not null &&
                                                                                                                   response.ContainsKey("action") && response.ContainsKey("result") && response.ContainsKey("message") &&
                                                                                                                   response.ContainsKey("actionString") && response.ContainsKey("resultString") && response.ContainsKey("messageString") &&
                                                                                                                   response.ContainsKey("dateTime");

    /// <summary>
    ///     Определяет тип ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <returns>Тип ответа <see cref="ApiResponseType" />.</returns>
    public static ApiResponseType GetResponseType(JObject? response) => response is null ? ApiResponseType.Undefined :
        response.ContainsKey("token") ? ApiResponseType.Token :
        response.ContainsKey("data") ? ApiResponseType.Data :
        response.ContainsKey("list") ? ApiResponseType.List : ApiResponseType.Undefined;

    /// <summary>
    ///     Пытается извлечь значения из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="responseType">Значение типа ответа <see cref="ApiResponseType" />.</param>
    /// <param name="action">Значение действия.</param>
    /// <param name="result">Значение результата.</param>
    /// <param name="message">Значение сообщения.</param>
    /// <param name="actionString">Строковое значение действия.</param>
    /// <param name="resultString">Строковое значение результата.</param>
    /// <param name="messageString">Строковое значение сообщения.</param>
    /// <param name="dateTime">Значение даты и времени.</param>
    /// <param name="token">Значение токена.</param>
    /// <param name="data">Данные в формате <see cref="JObject" />.</param>
    /// <param name="list">Список в формате <see cref="JArray" />.</param>
    /// <returns>Возвращает <c>true</c>, если значения успешно извлечены; иначе <c>false</c>.</returns>
    public static bool TryGetValues([NotNullWhen(true)] JObject? response, out ApiResponseType? responseType, out int? action, out int? result, out int? message, out string? actionString, out string? resultString, out string? messageString,
        out DateTime? dateTime,
        out string? token, out JObject? data, out JArray? list)
    {
        responseType  = null;
        action        = null;
        result        = null;
        message       = null;
        actionString  = null;
        resultString  = null;
        messageString = null;
        dateTime      = null;
        token         = null;
        data          = null;
        list          = null;
        if (!IsValidResponse(response, out var rt) ||
            !response.TryGetValue("action", StringComparison.Ordinal, out var actionJt) ||
            !response.TryGetValue("result", StringComparison.Ordinal, out var resultJt) ||
            !response.TryGetValue("message", StringComparison.Ordinal, out var messageJt) ||
            !response.TryGetValue("actionString", StringComparison.Ordinal, out var actionStringJt) ||
            !response.TryGetValue("resultString", StringComparison.Ordinal, out var resultStringJt) ||
            !response.TryGetValue("messageString", StringComparison.Ordinal, out var messageStringJt) ||
            !response.TryGetValue("dateTime", StringComparison.Ordinal, out var dateTimeJt))
            return false;
        switch (rt)
        {
            case ApiResponseType.Token:
                if (!response.TryGetValue("token", StringComparison.Ordinal, out var tokenJt))
                    return false;
                token = tokenJt.Value<string?>();
                break;
            case ApiResponseType.Data:
                if (!response.TryGetValue("data", StringComparison.Ordinal, out var dataJt))
                    return false;
                data = dataJt as JObject;
                break;
            case ApiResponseType.List:
                if (!response.TryGetValue("list", StringComparison.Ordinal, out var listJt))
                    return false;
                list = listJt as JArray;
                break;
            case ApiResponseType.Undefined:
            default:
                return false;
        }

        responseType  = rt;
        action        = actionJt.Value<int?>() ?? (int)ApiAction.None;
        result        = resultJt.Value<int?>() ?? (int)ApiResult.Failed;
        message       = messageJt.Value<int?>() ?? (int)ApiMessage.None;
        actionString  = actionStringJt.Value<string?>() ?? ApiAction.None.GetDescription();
        resultString  = resultStringJt.Value<string?>() ?? ApiResult.Failed.GetDescription();
        messageString = messageStringJt.Value<string?>() ?? ApiMessage.None.GetDescription();
        dateTime      = dateTimeJt.Value<DateTime?>();
        return true;
    }

    /// <summary>
    ///     Пытается извлечь значение действия из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="action">Значение действия.</param>
    /// <returns>Возвращает <c>true</c>, если значение действия успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetActionValue(JObject? response, out int? action) =>
        (action = response is not null && response.ContainsKey("action") && response.TryGetValue("action", StringComparison.Ordinal, out var actionJt)
            ? actionJt.Value<int?>() ?? (int)ApiAction.None
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь значение результата из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="result">Значение результата.</param>
    /// <returns>Возвращает <c>true</c>, если значение результата успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetResultValue(JObject? response, out int? result) =>
        (result = response is not null && response.ContainsKey("result") && response.TryGetValue("result", StringComparison.Ordinal, out var resultJt)
            ? resultJt.Value<int?>() ?? (int)ApiResult.Failed
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь значение сообщения из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="message">Значение сообщения.</param>
    /// <returns>Возвращает <c>true</c>, если значение сообщения успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetMessageValue(JObject? response, out int? message) =>
        (message = response is not null && response.ContainsKey("message") && response.TryGetValue("message", StringComparison.Ordinal, out var messageJt)
            ? messageJt.Value<int?>() ?? (int)ApiMessage.None
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь строковое значение действия из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="actionString">Строковое значение действия.</param>
    /// <returns>Возвращает <c>true</c>, если строковое значение действия успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetActionStringValue(JObject? response, out string? actionString) =>
        (actionString = response is not null && response.ContainsKey("actionString") && response.TryGetValue("actionString", StringComparison.Ordinal, out var actionStringJt)
            ? actionStringJt.Value<string?>() ?? ApiAction.None.GetDescription()
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь строковое значение результата из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="resultString">Строковое значение результата.</param>
    /// <returns>Возвращает <c>true</c>, если строковое значение результата успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetResultStringValue(JObject? response, out string? resultString) =>
        (resultString = response is not null && response.ContainsKey("resultString") && response.TryGetValue("resultString", StringComparison.Ordinal, out var resultStringJt)
            ? resultStringJt.Value<string?>() ?? ApiResult.Failed.GetDescription()
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь строковое значение сообщения из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="messageString">Строковое значение сообщения.</param>
    /// <returns>Возвращает <c>true</c>, если строковое значение сообщения успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetMessageStringValue(JObject? response, out string? messageString) =>
        (messageString = response is not null && response.ContainsKey("messageString") && response.TryGetValue("messageString", StringComparison.Ordinal, out var messageStringJt)
            ? messageStringJt.Value<string?>() ?? ApiMessage.None.GetDescription()
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь значение даты и времени из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="dateTime">Значение даты и времени.</param>
    /// <returns>Возвращает <c>true</c>, если значение даты и времени успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetDateTimeValue(JObject? response, out DateTime? dateTime) =>
        (dateTime = response is not null && response.ContainsKey("dateTime") && response.TryGetValue("dateTime", StringComparison.Ordinal, out var dateTimeJt)
            ? dateTimeJt.Value<DateTime?>()
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь значение токена из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="token">Значение токена.</param>
    /// <returns>Возвращает <c>true</c>, если значение токена успешно извлечено; иначе <c>false</c>.</returns>
    public static bool TryGetTokenValue(JObject? response, out string? token) =>
        (token = response is not null && response.ContainsKey("token") && response.TryGetValue("token", StringComparison.Ordinal, out var tokenJt)
            ? tokenJt.Value<string?>()
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь данные из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="data">Данные в формате <see cref="JObject" />.</param>
    /// <returns>Возвращает <c>true</c>, если данные успешно извлечены; иначе <c>false</c>.</returns>
    public static bool TryGetDataValue(JObject? response, out JObject? data) =>
        (data = response is not null && response.ContainsKey("data") && response.TryGetValue("data", StringComparison.Ordinal, out var dataJt)
            ? dataJt as JObject
            : null) is not null;

    /// <summary>
    ///     Пытается извлечь список из ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <param name="list">Список в формате <see cref="JArray" />.</param>
    /// <returns>Возвращает <c>true</c>, если список успешно извлечен; иначе <c>false</c>.</returns>
    public static bool TryGetListValue(JObject? response, out JArray? list) =>
        (list = response is not null && response.ContainsKey("list") && response.TryGetValue("list", StringComparison.Ordinal, out var listJt)
            ? listJt as JArray
            : null) is not null;

    /// <summary>
    ///     Выполняет асинхронный запрос к API.
    /// </summary>
    /// <param name="httpClient">Экземпляр <see cref="HttpClient" /> для выполнения запроса.</param>
    /// <param name="uri">URI для запроса.</param>
    /// <param name="parameters">Параметры запроса в формате <see cref="JObject" />.</param>
    /// <param name="cancellationToken">Токен отмены <see cref="CancellationToken" />.</param>
    /// <returns>Возвращает ответ в формате <see cref="JObject" />.</returns>
    public static async Task<JObject> ApiRequestAsync(HttpClient httpClient, Uri uri, JObject parameters, CancellationToken cancellationToken = default)
    {
        using var httpContent = new StringContent(parameters.ToString(), Encoding.UTF8, "application/json");
        using var response    = await httpClient.PostAsync(uri, httpContent, cancellationToken);
        return JObject.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
    }

    /// <summary>
    ///     Выполняет асинхронный запрос к API с тайм-аутом.
    /// </summary>
    /// <param name="httpClient">Экземпляр <see cref="HttpClient" /> для выполнения запроса.</param>
    /// <param name="uri">URI для запроса.</param>
    /// <param name="parameters">Параметры запроса в формате <see cref="JObject" />.</param>
    /// <param name="timeOut">Тайм-аут в миллисекундах.</param>
    /// <param name="cancellationToken">Токен отмены <see cref="CancellationToken" />.</param>
    /// <returns>Возвращает ответ в формате <see cref="JObject" />.</returns>
    public static async Task<JObject> ApiRequestAsync(HttpClient httpClient, Uri uri, JObject parameters, int timeOut, CancellationToken cancellationToken = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(timeOut);
        return await ApiRequestAsync(httpClient, uri, parameters, cts.Token);
    }
}