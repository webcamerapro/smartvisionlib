using System;
using eLibNet4Core.Interfaces;

namespace eLibNet4Core.Patterns
{
    /// <summary>
    ///     Абстрактный класс-паттерн для <see cref="IDisposable" />.
    /// </summary>
    public abstract class DisposablePattern : IDisposablePattern
    {
        /// <summary>
        ///     Возвращает <c>true</c> если объект был освобожден; иначе <c>false</c>.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Освобождает ресурсы, используемые объектом.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Выбрасывает исключение <see cref="ObjectDisposedException" />, если объект был освобожден.
        /// </summary>
        public void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>
        ///     Деструктор.
        /// </summary>
        ~DisposablePattern() => Dispose(false);

        /// <summary>
        ///     Освобождает ресурсы, используемые объектом. Вызывается из Dispose или деструктора.
        /// </summary>
        /// <param name="disposing">Значение, указывающее, вызывается ли метод из Dispose или деструктора.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;
            if (disposing) DisposeManagedResources();
            DisposeUnmanagedResources();
            IsDisposed = true;
        }

        /// <summary>
        ///     Освобождает управляемые ресурсы. Вызывается из Dispose.
        /// </summary>
        protected abstract void DisposeManagedResources();

        /// <summary>
        ///     Освобождает неуправляемые ресурсы. Вызывается из Dispose.
        /// </summary>
        protected abstract void DisposeUnmanagedResources();
    }
}