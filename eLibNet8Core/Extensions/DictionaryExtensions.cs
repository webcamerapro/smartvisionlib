namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы расширения для словарей.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    ///     Проверяет, равны ли два словаря по содержимому.
    /// </summary>
    /// <typeparam name="TKey">Тип ключей словаря.</typeparam>
    /// <typeparam name="TValue">Тип значений словаря.</typeparam>
    /// <param name="target">Первый словарь для сравнения.</param>
    /// <param name="other">Второй словарь для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если словари равны; иначе <c>false</c>.</returns>
    public static bool ForeachEquals<TKey, TValue>(this IDictionary<TKey, TValue> target, IDictionary<TKey, TValue> other) where TKey : notnull where TValue : IEquatable<TValue>
    {
        if (target.Count != other.Count)
            return false;
        foreach (var kvp in target)
            if (!other.TryGetValue(kvp.Key, out var value) || !value.Equals(kvp.Value))
                return false;
        return true;
    }
}