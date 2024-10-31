using eLibNet8VideoSurveillanceApi.Enumerations;
using Newtonsoft.Json.Linq;

namespace eLibNet8VideoSurveillanceApi.Interfaces;

/// <summary>
///     Интерфейс класса, представляющего ответ на API запрос.
/// </summary>
public interface IApiResponse : IEquatable<IApiResponse>
{
    /// <summary>
    ///     Возвращает значение, указывающее, инициализирован ли экземпляр класса.
    /// </summary>
    public bool IsInitialized { get; }

    /// <summary>
    ///     Тип ответа.
    /// </summary>
    public ApiResponseType? ResponseType { get; }

    /// <summary>
    ///     Запрошенное действие.
    /// </summary>
    public int? Action { get; }

    /// <summary>
    ///     Полученный результат.
    /// </summary>
    public int? Result { get; }

    /// <summary>
    ///     Полученное сообщение.
    /// </summary>
    public int? Message { get; }

    /// <summary>
    ///     Запрошенное действие в виде строки.
    /// </summary>
    public string? ActionString { get; }

    /// <summary>
    ///     Полученный результат в виде строки.
    /// </summary>
    public string? ResultString { get; }

    /// <summary>
    ///     Полученное сообщение в виде строки.
    /// </summary>
    public string? MessageString { get; }

    /// <summary>
    ///     Запрошенное действие в виде перечисления <see cref="ApiAction" />.
    /// </summary>
    public ApiAction? ActionEnum { get; }

    /// <summary>
    ///     Полученный результат в виде перечисления <see cref="ApiResult" />.
    /// </summary>
    public ApiResult? ResultEnum { get; }

    /// <summary>
    ///     Полученное сообщение в виде перечисления <see cref="ApiMessage" />.
    /// </summary>
    public ApiMessage? MessageEnum { get; }

    /// <summary>
    ///     Полученные дата и время завершения выполнения действия.
    /// </summary>
    public DateTime? DateTime { get; }

    /// <summary>
    ///     Полученный токен.
    /// </summary>
    public string? Token { get; }

    /// <summary>
    ///     Полученные данные <see cref="JObject" />.
    /// </summary>
    public JObject? Data { get; }

    /// <summary>
    ///     Полученный список <see cref="JArray" />.
    /// </summary>
    public JArray? List { get; }

    /// <summary>
    ///     Инициализирует экземпляр класса, заполняя данные API ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <returns>Возвращает текущий экземпляр <see cref="IApiResponse" />.</returns>
    /// <remarks>Автоматически вызывается при передаче API ответа в конструктор. Во избежание исключений, рекомендуется проверить <see cref="IsInitialized" /> на ложность перед вызовом.</remarks>
    public IApiResponse Initialize(JObject response);
}