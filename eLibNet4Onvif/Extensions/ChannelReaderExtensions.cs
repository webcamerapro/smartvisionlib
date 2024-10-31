using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using Dasync.Collections;

namespace eLibNet4Onvif.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для <see cref="ChannelReader{T}" />.
    /// </summary>
    public static class ChannelReaderExtensions
    {
        /// <summary>
        ///     Асинхронно читает все элементы из ChannelReader.
        /// </summary>
        /// <typeparam name="T">Тип элементов в ChannelReader.</typeparam>
        /// <param name="target">ChannelReader, из которого читаются элементы.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Асинхронный перечислитель элементов.</returns>
        public static IAsyncEnumerable<T> ReadAllAsync<T>(this ChannelReader<T> target, CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerable<T>(async yield =>
            {
                while (await target.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
                    while (target.TryRead(out var item))
                        await yield.ReturnAsync(item);
            });
        }
    }
}