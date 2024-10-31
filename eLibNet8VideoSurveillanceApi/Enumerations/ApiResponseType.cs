namespace eLibNet8VideoSurveillanceApi.Enumerations;

/// <summary>
///     Перечисление, представляющее различные типы ответов API.
/// </summary>
public enum ApiResponseType
{
    /// <summary>
    ///     Неопределенный тип ответа.
    /// </summary>
    Undefined = 0,

    /// <summary>
    ///     Ответ с токеном.
    /// </summary>
    Token = 1,

    /// <summary>
    ///     Ответ с данными.
    /// </summary>
    Data = 2,

    /// <summary>
    ///     Ответ со списком.
    /// </summary>
    List = 3
}