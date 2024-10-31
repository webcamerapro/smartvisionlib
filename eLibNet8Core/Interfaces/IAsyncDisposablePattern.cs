namespace eLibNet8Core.Interfaces;

/// <summary>
///     Интерфейс, представляющий шаблон для асинхронного освобождения ресурсов, объединяющий IDisposablePattern и IAsyncDisposable.
/// </summary>
public interface IAsyncDisposablePattern : IDisposablePattern, IAsyncDisposable;