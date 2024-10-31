using System.ComponentModel;

namespace eLibNet8VideoSurveillanceApi.Enumerations;

/// <summary>
///     Перечисление, представляющее различные результаты API.
/// </summary>
public enum ApiResult
{
    /// <summary>
    ///     Неудача.
    /// </summary>
    [Description("failed")]
    Failed = 0,

    /// <summary>
    ///     Успех.
    /// </summary>
    [Description("success")]
    Success = 1,

    /// <summary>
    ///     Незавершенный.
    /// </summary>
    [Description("incomplete")]
    Incomplete = 2
}