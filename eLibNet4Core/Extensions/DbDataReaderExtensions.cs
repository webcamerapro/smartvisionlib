using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace eLibNet4Core.Extensions
{
    /// <summary>
    ///     Класс, содержащий методы-расширения для <see cref="DbDataReader" />.
    /// </summary>
    public static class DbDataReaderExtensions
    {
        /// <summary>
        ///     Получает значение поля указанного типа из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <typeparam name="T">Тип значения поля.</typeparam>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение поля указанного типа.</returns>
        public static T GetFieldValue<T>(this DbDataReader target, string columnName) => target.GetFieldValue<T>(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение поля указанного типа из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <typeparam name="T">Тип значения поля.</typeparam>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение поля указанного типа.</returns>
        public static async Task<T> GetFieldValueAsync<T>(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<T>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="string" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="string" />.</returns>
        public static string GetString(this DbDataReader target, string columnName) => target.GetString(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="string" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="string" />.</returns>
        public static async Task<string> GetStringAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<string>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="string" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="string" />.</returns>
        public static async Task<string> GetStringAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<string>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="string" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="string" />.</returns>
        public static string GetNString(this DbDataReader target, int index) => target.IsDBNull(index) ? null : target.GetString(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="string" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="string" />.</returns>
        public static string GetNString(this DbDataReader target, string columnName) => target.GetNString(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="string" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="string" />.</returns>
        public static async Task<string> GetNStringAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? null : await target.GetStringAsync(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="string" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="string" />.</returns>
        public static async Task<string> GetNStringAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNStringAsync(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="bool" />.</returns>
        public static bool GetBoolean(this DbDataReader target, string columnName) => target.GetBoolean(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="bool" />.</returns>
        public static async Task<bool> GetBooleanAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<bool>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="bool" />.</returns>
        public static async Task<bool> GetBooleanAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<bool>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="bool" />.</returns>
        public static bool? GetNBoolean(this DbDataReader target, int index) => target.IsDBNull(index) ? (bool?)null : target.GetBoolean(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="bool" />.</returns>
        public static bool? GetNBoolean(this DbDataReader target, string columnName) => target.GetNBoolean(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="bool" />.</returns>
        public static async Task<bool?> GetNBooleanAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (bool?)null : await target.GetBooleanAsync(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="bool" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="bool" />.</returns>
        public static async Task<bool?> GetNBooleanAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNBooleanAsync(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="short" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="short" />.</returns>
        public static short GetInt16(this DbDataReader target, string columnName) => target.GetInt16(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="short" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="short" />.</returns>
        public static async Task<short> GetInt16Async(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<short>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="short" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="short" />.</returns>
        public static async Task<short> GetInt16Async(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<short>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="short" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="short" />.</returns>
        public static short? GetNInt16(this DbDataReader target, int index) => target.IsDBNull(index) ? (short?)null : target.GetInt16(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="short" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="short" />.</returns>
        public static short? GetNInt16(this DbDataReader target, string columnName) => target.GetNInt16(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="short" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="short" />.</returns>
        public static async Task<short?> GetNInt16Async(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (short?)null : await target.GetInt16Async(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="short" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="short" />.</returns>
        public static async Task<short?> GetNInt16Async(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNInt16Async(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="int" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="int" />.</returns>
        public static int GetInt32(this DbDataReader target, string columnName) => target.GetInt32(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="int" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="int" />.</returns>
        public static async Task<int> GetInt32Async(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<int>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="int" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="int" />.</returns>
        public static async Task<int> GetInt32Async(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<int>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="int" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="int" />.</returns>
        public static int? GetNInt32(this DbDataReader target, int index) => target.IsDBNull(index) ? (int?)null : target.GetInt32(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="int" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="int" />.</returns>
        public static int? GetNInt32(this DbDataReader target, string columnName) => target.GetNInt32(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="int" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="int" />.</returns>
        public static async Task<int?> GetNInt32Async(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (int?)null : await target.GetInt32Async(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="int" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="int" />.</returns>
        public static async Task<int?> GetNInt32Async(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNInt32Async(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="long" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="long" />.</returns>
        public static long GetInt64(this DbDataReader target, string columnName) => target.GetInt64(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="long" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="long" />.</returns>
        public static async Task<long> GetInt64Async(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<long>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="long" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="long" />.</returns>
        public static async Task<long> GetInt64Async(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<long>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="long" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="long" />.</returns>
        public static long? GetNInt64(this DbDataReader target, int index) => target.IsDBNull(index) ? (long?)null : target.GetInt64(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="long" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="long" />.</returns>
        public static long? GetNInt64(this DbDataReader target, string columnName) => target.GetNInt64(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="long" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="long" />.</returns>
        public static async Task<long?> GetNInt64Async(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (long?)null : await target.GetInt64Async(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="long" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="long" />.</returns>
        public static async Task<long?> GetNInt64Async(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNInt64Async(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="DateTime" />.</returns>
        public static DateTime GetDateTime(this DbDataReader target, string columnName) => target.GetDateTime(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="DateTime" />.</returns>
        public static async Task<DateTime> GetDateTimeAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<DateTime>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="DateTime" />.</returns>
        public static async Task<DateTime> GetDateTimeAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<DateTime>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="DateTime" />.</returns>
        public static DateTime? GetNDateTime(this DbDataReader target, int index) => target.IsDBNull(index) ? (DateTime?)null : target.GetDateTime(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="DateTime" />.</returns>
        public static DateTime? GetNDateTime(this DbDataReader target, string columnName) => target.GetNDateTime(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="DateTime" />.</returns>
        public static async Task<DateTime?> GetNDateTimeAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (DateTime?)null : await target.GetDateTimeAsync(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="DateTime" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="DateTime" />.</returns>
        public static async Task<DateTime?> GetNDateTimeAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNDateTimeAsync(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="object" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="object" />.</returns>
        public static object GetValue(this DbDataReader target, string columnName) => target.GetValue(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="object" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="object" />.</returns>
        public static async Task<object> GetValueAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<object>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="object" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="object" />.</returns>
        public static async Task<object> GetValueAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<object>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="object" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="object" />.</returns>
        public static object GetNValue(this DbDataReader target, int index) => target.IsDBNull(index) ? null : target.GetValue(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="object" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="object" />.</returns>
        public static object GetNValue(this DbDataReader target, string columnName) => target.GetNValue(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="object" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="object" />.</returns>
        public static async Task<object> GetNValueAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? null : await target.GetValueAsync(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="object" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="object" />.</returns>
        public static async Task<object> GetNValueAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNValueAsync(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="float" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="float" />.</returns>
        public static float GetFloat(this DbDataReader target, string columnName) => target.GetFloat(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="float" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="float" />.</returns>
        public static async Task<float> GetFloatAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<float>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="float" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="float" />.</returns>
        public static async Task<float> GetFloatAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<float>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="float" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="float" />.</returns>
        public static float? GetNFloat(this DbDataReader target, int index) => target.IsDBNull(index) ? (float?)null : target.GetFloat(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="float" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="float" />.</returns>
        public static float? GetNFloat(this DbDataReader target, string columnName) => target.GetNFloat(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="float" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="float" />.</returns>
        public static async Task<float?> GetNFloatAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (float?)null : await target.GetFloatAsync(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="float" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="float" />.</returns>
        public static async Task<float?> GetNFloatAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNFloatAsync(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Значение типа <see cref="decimal" />.</returns>
        public static decimal GetDecimal(this DbDataReader target, string columnName) => target.GetDecimal(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="decimal" />.</returns>
        public static async Task<decimal> GetDecimalAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<decimal>(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Значение типа <see cref="decimal" />.</returns>
        public static async Task<decimal> GetDecimalAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetFieldValueAsync<decimal>(target.GetOrdinal(columnName), cancellationToken);

        /// <summary>
        ///     Получает nullable значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <returns>Nullable значение типа <see cref="decimal" />.</returns>
        public static decimal? GetNDecimal(this DbDataReader target, int index) => target.IsDBNull(index) ? (decimal?)null : target.GetDecimal(index);

        /// <summary>
        ///     Получает nullable значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Nullable значение типа <see cref="decimal" />.</returns>
        public static decimal? GetNDecimal(this DbDataReader target, string columnName) => target.GetNDecimal(target.GetOrdinal(columnName));

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по индексу столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="index">Индекс столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="decimal" />.</returns>
        public static async Task<decimal?> GetNDecimalAsync(this DbDataReader target, int index, CancellationToken cancellationToken = default) =>
            await target.IsDBNullAsync(index, cancellationToken) ? (decimal?)null : await target.GetDecimalAsync(index, cancellationToken);

        /// <summary>
        ///     Асинхронно получает nullable значение типа <see cref="decimal" /> из <see cref="DbDataReader" /> по имени столбца.
        /// </summary>
        /// <param name="target">Экземпляр <see cref="DbDataReader" />.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Nullable значение типа <see cref="decimal" />.</returns>
        public static async Task<decimal?> GetNDecimalAsync(this DbDataReader target, string columnName, CancellationToken cancellationToken = default) => await target.GetNDecimalAsync(target.GetOrdinal(columnName), cancellationToken);
    }
}