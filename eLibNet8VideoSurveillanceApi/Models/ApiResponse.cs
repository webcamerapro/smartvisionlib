using System.Diagnostics.CodeAnalysis;
using eLibNet8Core.Helpers;
using eLibNet8VideoSurveillanceApi.Enumerations;
using eLibNet8VideoSurveillanceApi.Helpers;
using eLibNet8VideoSurveillanceApi.Interfaces;
using Newtonsoft.Json.Linq;

namespace eLibNet8VideoSurveillanceApi.Models;

/// <summary>
///     Класс, представляющий ответ на API запрос.
/// </summary>
public class ApiResponse : IApiResponse
{
    private int? _action;
    private string? _actionString;
    private JObject? _data;
    private DateTime? _dateTime;
    private JArray? _list;
    private int? _message;
    private string? _messageString;
    private ApiResponseType? _responseType;
    private int? _result;
    private string? _resultString;
    private string? _token;

    /// <summary>
    ///     Конструктор класса <see cref="ApiResponse" /> без инициализации.
    /// </summary>
    /// <remarks>Для работы с классом необходимо отдельно выполнить инициализацию вызвав метод <see cref="Initialize" />.</remarks>
    public ApiResponse() => IsInitialized = false;

    /// <summary>
    ///     Конструктор класса <see cref="ApiResponse" /> с инициализацией.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <remarks>Отдельно вызывать инициализацию методом <see cref="Initialize" /> не нужно.</remarks>
    public ApiResponse(JObject response) : this() => Initialize(response);

    /// <summary>
    ///     Возвращает значение, указывающее, инициализирован ли экземпляр класса.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    ///     Тип ответа.
    /// </summary>
    public ApiResponseType? ResponseType => _responseType;

    /// <summary>
    ///     Запрошенное действие.
    /// </summary>
    public int? Action => _action;

    /// <summary>
    ///     Полученный результат.
    /// </summary>
    public int? Result => _result;

    /// <summary>
    ///     Полученное сообщение.
    /// </summary>
    public int? Message => _message;

    /// <summary>
    ///     Запрошенное действие в виде строки.
    /// </summary>
    public string? ActionString => _actionString;

    /// <summary>
    ///     Полученный результат в виде строки.
    /// </summary>
    public string? ResultString => _resultString;

    /// <summary>
    ///     Полученное сообщение в виде строки.
    /// </summary>
    public string? MessageString => _messageString;

    /// <summary>
    ///     Запрошенное действие в виде перечисления <see cref="ApiAction" />.
    /// </summary>
    public ApiAction? ActionEnum => EnumHelper.IsDefined<ApiAction>(Action) ? (ApiAction)Action : null;

    /// <summary>
    ///     Полученный результат в виде перечисления <see cref="ApiResult" />.
    /// </summary>
    public ApiResult? ResultEnum => EnumHelper.IsDefined<ApiResult>(Result) ? (ApiResult)Result : null;

    /// <summary>
    ///     Полученное сообщение в виде перечисления <see cref="ApiMessage" />.
    /// </summary>
    public ApiMessage? MessageEnum => EnumHelper.IsDefined<ApiMessage>(Message) ? (ApiMessage)Message : null;

    /// <summary>
    ///     Полученные дата и время завершения выполнения действия.
    /// </summary>
    public DateTime? DateTime => _dateTime;

    /// <summary>
    ///     Полученный токен.
    /// </summary>
    public string? Token => _token;

    /// <summary>
    ///     Полученные данные <see cref="JObject" />.
    /// </summary>
    public JObject? Data => _data;

    /// <summary>
    ///     Полученный список <see cref="JArray" />.
    /// </summary>
    public JArray? List => _list;

    /// <summary>
    ///     Инициализирует экземпляр класса, заполняя данные API ответа.
    /// </summary>
    /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
    /// <returns>Возвращает текущий экземпляр <see cref="IApiResponse" />.</returns>
    /// <exception cref="Exception">Возвращает исключение в случае если экземпляр класса уже был инициализирован, или в случае неверного формата API ответа.</exception>
    /// <remarks>Автоматически вызывается при передаче API ответа в конструктор. Во избежание исключений, рекомендуется проверить <see cref="IsInitialized" /> на ложность перед вызовом.</remarks>
    public IApiResponse Initialize(JObject response)
    {
        if (IsInitialized)
            ExceptionHelper.ThrowException("Повторная инициализация невозможна.");
        if (!ApiHelper.TryGetValues(response, out _responseType, out _action, out _result, out _message, out _actionString, out _resultString, out _messageString, out _dateTime, out _token, out _data, out _list))
            ExceptionHelper.ThrowException("Неверный формат API ответа.");
        IsInitialized = true;
        return this;
    }

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту того же типа.
    /// </summary>
    /// <param name="other">Другой объект <see cref="IApiResponse" /> для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если текущий объект равен <paramref name="other" />; иначе <c>false</c>.</returns>
    public bool Equals(IApiResponse? other) =>
        other is not null &&
        IsInitialized == other.IsInitialized &&
        ResponseType == other.ResponseType &&
        Action == other.Action &&
        Result == other.Result &&
        Message == other.Message &&
        ActionString == other.ActionString &&
        ResultString == other.ResultString &&
        MessageString == other.MessageString &&
        DateTime == other.DateTime &&
        Token == other.Token &&
        Data == other.Data &&
        List == other.List;

    /// <summary>
    ///     Определяет, равен ли текущий объект другому объекту.
    /// </summary>
    /// <param name="obj">Другой объект для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если текущий объект равен параметру <paramref name="obj" />; иначе <c>false</c>.</returns>
    public override bool Equals(object? obj) => Equals(obj as IApiResponse);

    /// <summary>
    ///     Возвращает хэш-код для текущего объекта.
    /// </summary>
    /// <returns>Хэш-код для текущего объекта.</returns>
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        if (!IsInitialized)
            return HashCode.Combine(nameof(IApiResponse));
        HashCode hash = new();
        hash.Add(ResponseType);
        hash.Add(Action);
        hash.Add(Result);
        hash.Add(Message);
        hash.Add(ActionString);
        hash.Add(ResultString);
        hash.Add(MessageString);
        hash.Add(DateTime);
        hash.Add(Data);
        hash.Add(List);
        hash.Add(Token);
        return hash.ToHashCode();
    }
}