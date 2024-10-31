using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace eLibNet4Onvif.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для <see cref="IObservable{T}" />.
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        ///     Завершает <see cref="IObservable{T}" /> при отмене через CancellationToken.
        /// </summary>
        /// <typeparam name="T">Тип элементов в последовательности.</typeparam>
        /// <param name="target">Исходная последовательность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Последовательность, которая прерывается при отмене.</returns>
        public static IObservable<T> TakeUntil<T>(this IObservable<T> target, CancellationToken cancellationToken) => target.TakeUntil(Observable.Create<Unit>(o => cancellationToken.Register(() => o.OnNext(default))));

        /// <summary>
        ///     Завершает <see cref="IObservable{T}" /> с ошибкой при отмене через CancellationToken.
        /// </summary>
        /// <typeparam name="T">Тип элементов в последовательности.</typeparam>
        /// <param name="target">Исходная последовательность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Последовательность, которая прерывается с ошибкой при отмене.</returns>
        public static IObservable<T> WithCancellation<T>(this IObservable<T> target, CancellationToken cancellationToken) =>
            target.TakeUntil(Observable.Create<Unit>(o => cancellationToken.Register(() => o.OnError(new OperationCanceledException(cancellationToken)))));
    }
}