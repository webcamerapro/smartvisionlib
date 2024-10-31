using System.Runtime.CompilerServices;
using System.Text;
using eLibNet8Core.Extensions;
using Newtonsoft.Json;

namespace eLibNet8Core.Helpers;

/// <summary>
///     Класс, предоставляющий методы для создания и форматирования строк журнала с дополнительной информацией о вызывающем методе.
/// </summary>
public static class LogHelper
{
    private static string LogString(string message, string callerFilePath, string callerMemberName, string callerLineNumber)
    {
        var       filePath   = Path.GetFileNameWithoutExtension(callerFilePath);
        var       sb         = new StringBuilder();
        using var sw         = new StringWriter(sb);
        using var jsonWriter = new JsonTextWriter(sw);
        jsonWriter.WriteStartObject();
        if (!filePath.IsEmpty())
        {
            jsonWriter.WritePropertyName("Файл");
            jsonWriter.WriteValue(filePath);
        }

        if (!callerMemberName.IsEmpty())
        {
            jsonWriter.WritePropertyName("Элемент");
            jsonWriter.WriteValue(callerMemberName);
        }

        if (!callerLineNumber.IsEmpty())
        {
            jsonWriter.WritePropertyName("Строка");
            jsonWriter.WriteValue(callerLineNumber);
        }

        jsonWriter.WritePropertyName("Сообщение");
        jsonWriter.WriteValue(message);
        jsonWriter.WriteEndObject();
        return sb.ToString();
    }

    /// <summary>
    ///     Возвращает строку журнала с указанным сообщением и информацией о вызывающем методе.
    /// </summary>
    /// <param name="message">Сообщение журнала.</param>
    /// <param name="callerFilePath">Путь к файлу, из которого вызван метод (автоматически заполняется).</param>
    /// <param name="callerMemberName">Имя метода, из которого вызван метод (автоматически заполняется).</param>
    /// <param name="callerLineNumber">Номер строки, из которой вызван метод (автоматически заполняется).</param>
    /// <returns>Сформированная строка журнала.</returns>
    public static string RegularString(string message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0) =>
        LogString(message, callerFilePath, callerMemberName, callerLineNumber.ToString());

    /// <summary>
    ///     Возвращает строку журнала с информацией об ошибке.
    /// </summary>
    /// <param name="exception">Исключение, из которого извлекается сообщение.</param>
    /// <param name="callerFilePath">Путь к файлу, из которого вызван метод (автоматически заполняется).</param>
    /// <param name="callerMemberName">Имя метода, из которого вызван метод (автоматически заполняется).</param>
    /// <param name="callerLineNumber">Номер строки, из которой вызван метод (автоматически заполняется).</param>
    /// <returns>Сформированная строка журнала. Если у <see cref="Exception" /> отсутствуют данные о пути к файлу, имени метода или номере строки, они будут заменены таковыми из этого метода.</returns>
    public static string ExceptionString(Exception exception, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0) =>
        LogString(exception.Message, ExceptionHelper.GetExceptionData(exception, "CallerFilePath") ?? callerFilePath, ExceptionHelper.GetExceptionData(exception, "CallerMemberName") ?? callerMemberName,
            ExceptionHelper.GetExceptionData(exception, "CallerLineNumber") ?? callerLineNumber.ToString());
}