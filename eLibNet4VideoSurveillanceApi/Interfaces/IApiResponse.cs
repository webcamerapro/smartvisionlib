using System;
using eLibNet4VideoSurveillanceApi.Enumerations;
using Newtonsoft.Json.Linq;

namespace eLibNet4VideoSurveillanceApi.Interfaces
{
    /// <summary>
    ///     Интерфейс класса, представляющего ответ на API запрос.
    /// </summary>
    public interface IApiResponse : IEquatable<IApiResponse>
    {
        /// <summary>
        ///     Возвращает значение, указывающее, инициализирован ли экземпляр класса.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        ///     Тип ответа.
        /// </summary>
        ApiResponseType? ResponseType { get; }

        /// <summary>
        ///     Запрошенное действие.
        /// </summary>
        int? Action { get; }

        /// <summary>
        ///     Полученный результат.
        /// </summary>
        int? Result { get; }

        /// <summary>
        ///     Полученное сообщение.
        /// </summary>
        int? Message { get; }

        /// <summary>
        ///     Запрошенное действие в виде строки.
        /// </summary>
        string ActionString { get; }

        /// <summary>
        ///     Полученный результат в виде строки.
        /// </summary>
        string ResultString { get; }

        /// <summary>
        ///     Полученное сообщение в виде строки.
        /// </summary>
        string MessageString { get; }

        /// <summary>
        ///     Запрошенное действие в виде перечисления <see cref="ApiAction" />.
        /// </summary>
        ApiAction? ActionEnum { get; }

        /// <summary>
        ///     Полученный результат в виде перечисления <see cref="ApiResult" />.
        /// </summary>
        ApiResult? ResultEnum { get; }

        /// <summary>
        ///     Полученное сообщение в виде перечисления <see cref="ApiMessage" />.
        /// </summary>
        ApiMessage? MessageEnum { get; }

        /// <summary>
        ///     Полученные дата и время завершения выполнения действия.
        /// </summary>
        DateTime? DateTime { get; }

        /// <summary>
        ///     Полученный токен.
        /// </summary>
        string Token { get; }

        /// <summary>
        ///     Полученные данные <see cref="JObject" />.
        /// </summary>
        JObject Data { get; }

        /// <summary>
        ///     Полученный список <see cref="JArray" />.
        /// </summary>
        JArray List { get; }

        /// <summary>
        ///     Инициализирует экземпляр класса, заполняя данные API ответа.
        /// </summary>
        /// <param name="response">Ответ в формате <see cref="JObject" />.</param>
        /// <returns>Возвращает текущий экземпляр <see cref="IApiResponse" />.</returns>
        /// <remarks>Автоматически вызывается при передаче API ответа в конструктор. Во избежание исключений, рекомендуется проверить <see cref="IsInitialized" /> на ложность перед вызовом.</remarks>
        IApiResponse Initialize(JObject response);
    }
}