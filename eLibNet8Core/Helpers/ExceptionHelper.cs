using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace eLibNet8Core.Helpers;

/// <summary>
///     Класс, предоставляющий методы для создания исключений с дополнительной информацией о вызывающем методе.
/// </summary>
public static class ExceptionHelper
{
    private static Exception NewException(string? message, int? code, string callerFilePath, string callerMemberName, int callerLineNumber)
    {
        var exception = new Exception(message);
        if (code is not null)
            exception.Data.Add("Code", code);
        exception.Data.Add("CallerFilePath", callerFilePath);
        exception.Data.Add("CallerMemberName", callerMemberName);
        exception.Data.Add("CallerLineNumber", callerLineNumber);
        return exception;
    }

    private static ArgumentException NewArgumentException(string? message, int? code, string? paramName, Exception? innerException, string callerFilePath, string callerMemberName, int callerLineNumber)
    {
        var argumentException = new ArgumentException(message, paramName, innerException);
        if (code is not null)
            argumentException.Data.Add("Code", code);
        argumentException.Data.Add("CallerFilePath", callerFilePath);
        argumentException.Data.Add("CallerMemberName", callerMemberName);
        argumentException.Data.Add("CallerLineNumber", callerLineNumber);
        return argumentException;
    }

    /// <summary>
    ///     Выбрасывает новое исключение <see cref="Exception" /> с указанным сообщением и информацией о вызывающем методе.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="callerFilePath">Путь к файлу, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerMemberName">Имя метода, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerLineNumber">Номер строки, из которой вызвано исключение (автоматически заполняется).</param>
    /// <returns>Созданное исключение <see cref="Exception" />.</returns>
    [DoesNotReturn]
    public static Exception ThrowException(string? message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0) =>
        throw NewException(message, null, callerFilePath, callerMemberName, callerLineNumber);

    /// <summary>
    ///     Выбрасывает новое исключение <see cref="Exception" /> с указанным сообщением, кодом и информацией о вызывающем методе.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="code">Код (может быть null).</param>
    /// <param name="callerFilePath">Путь к файлу, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerMemberName">Имя метода, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerLineNumber">Номер строки, из которой вызвано исключение (автоматически заполняется).</param>
    /// <returns>Созданное исключение <see cref="Exception" />.</returns>
    [DoesNotReturn]
    public static Exception ThrowException(string? message, int? code, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0) =>
        throw NewException(message, code, callerFilePath, callerMemberName, callerLineNumber);

    /// <summary>
    ///     Выбрасывает новое исключение <see cref="ArgumentException" /> с указанным сообщением, кодом и информацией о вызывающем методе.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="paramName">Имя аргумента.</param>
    /// <param name="innerException">Внутреннее исключение <see cref="Exception" />.</param>
    /// <param name="callerFilePath">Путь к файлу, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerMemberName">Имя метода, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerLineNumber">Номер строки, из которой вызвано исключение (автоматически заполняется).</param>
    /// <returns>Созданное исключение <see cref="ArgumentException" />.</returns>
    [DoesNotReturn]
    public static ArgumentException ThrowArgumentException(string? message, string? paramName = null, Exception? innerException = null,
        [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0) =>
        throw NewArgumentException(message, null, paramName, innerException, callerFilePath, callerMemberName, callerLineNumber);

    /// <summary>
    ///     Выбрасывает новое исключение <see cref="ArgumentException" /> с указанным сообщением, кодом и информацией о вызывающем методе.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="code">Код (может быть null).</param>
    /// <param name="paramName">Имя аргумента.</param>
    /// <param name="innerException">Внутреннее исключение <see cref="Exception" />.</param>
    /// <param name="callerFilePath">Путь к файлу, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerMemberName">Имя метода, из которого вызвано исключение (автоматически заполняется).</param>
    /// <param name="callerLineNumber">Номер строки, из которой вызвано исключение (автоматически заполняется).</param>
    /// <returns>Созданное исключение <see cref="ArgumentException" />.</returns>
    [DoesNotReturn]
    public static ArgumentException ThrowArgumentException(string? message, int? code, string? paramName = null, Exception? innerException = null,
        [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0) =>
        throw NewArgumentException(message, code, paramName, innerException, callerFilePath, callerMemberName, callerLineNumber);

    /// <summary>
    ///     Возвращает дополнительные данные по указанному имени.
    /// </summary>
    /// <param name="exception">Исключение, из которого извлекаются данные.</param>
    /// <param name="name">Имя параметра.</param>
    /// <returns>Данные согласно имени параметра если оно существует; иначе <c>null</c>.</returns>
    public static string? GetExceptionData(Exception exception, string name) => !exception.Data.Contains(name) ? null : exception.Data[name]?.ToString();
}