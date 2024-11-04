namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы-расширения для <see cref="Uri" />.
/// </summary>
public static class UriExtensions
{
    /// <summary>
    ///     Возвращает сегмент URI, следующий сразу за указанным сегментом.
    /// </summary>
    /// <param name="target">Целевой URI.</param>
    /// <param name="segment">Сегмент, после которого нужно найти следующий сегмент.</param>
    /// <returns>Сегмент, следующий сразу за указанным сегментом, или null, если сегмент не найден.</returns>
    public static string? GetRightAfterSegment(this Uri target, string segment)
    {
        string? previousSegment = null;
        var     trimmedSegment  = segment.TrimEnd('/', '\\');
        foreach (var s in target.Segments.Select(str => str.TrimEnd('/', '\\')))
        {
            if (previousSegment == trimmedSegment)
                return s;
            previousSegment = s;
        }

        return null;
    }

    /// <summary>
    ///     Возвращает сегмент URI, следующий за найденным сегментом из указанного списка сегментов.
    /// </summary>
    /// <param name="target">Целевой URI.</param>
    /// <param name="segments">Массив сегментов, после которых нужно найти следующий сегмент.</param>
    /// <returns>Сегмент, следующий за найденным сегментом из указанного списка сегментов, или null, если сегмент не найден.</returns>
    public static string? GetRightAfterSegments(this Uri target, params string[] segments)
    {
        string? previousSegment = null;
        var     trimmedSegments = segments.Select(str => str.TrimEnd('/', '\\')).ToArray();
        foreach (var s in target.Segments.Select(str => str.TrimEnd('/', '\\')))
        {
            if (trimmedSegments.Contains(previousSegment))
                return s;
            previousSegment = s;
        }

        return null;
    }

    /// <summary>
    ///     Возвращает сегмент URI, следующий за найденным сегментом из указанного списка сегментов согласно приоритету порядка очереди.
    /// </summary>
    /// <param name="target">Целевой URI.</param>
    /// <param name="segments">Массив сегментов, после которых нужно найти следующий сегмент.</param>
    /// <returns>Сегмент, следующий за найденным сегментом из указанного списка сегментов согласно приоритету порядка очереди, или null, если сегмент не найден.</returns>
    public static string? GetRightAfterSegmentsPriority(this Uri target, params string[] segments) => segments.Select(segment => GetRightAfterSegment(target, segment)).FirstOrDefault(result => result != null);
}