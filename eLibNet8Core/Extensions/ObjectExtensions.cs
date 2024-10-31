namespace eLibNet8Core.Extensions;

/// <summary>
///     Класс, содержащий методы-расширения для <see cref="object" />.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    ///     Проверяет, равен ли текущий объект другому объекту. Поддерживает сравнение <c>null</c> с обеих сторон.
    /// </summary>
    /// <param name="target">Текущий объект.</param>
    /// <param name="other">Объект для сравнения.</param>
    /// <returns>Значение <c>true</c>, если объекты равны; иначе <c>false</c>. Если оба объекта равны <c>null</c>, они считаются равными между собой.</returns>
    public static bool NullableEquals(this object? target, object? other) => target?.Equals(other) ?? other == null;
}