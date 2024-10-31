using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;

namespace eLibNet4Core.Models
{
    /// <summary>
    ///     Представляет потокобезопасный словарь, который поддерживает уведомления об изменениях коллекции.
    /// </summary>
    /// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
    /// <typeparam name="TValue">Тип значений в словаре.</typeparam>
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Serializable]
    public class ObservableConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly SynchronizationContext _synchronizationContext = AsyncOperationManager.SynchronizationContext;

        void IDictionary.Add(object key, [CanBeNull] object value)
        {
            if (!(key is TKey typedKey))
                throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
            switch (value)
            {
                case TValue typedValue:
                    TryAdd(typedKey, typedValue);
                    break;
                case null:
                    TryAdd(typedKey, default);
                    break;
                default:
                    throw new ArgumentException("Тип параметра value не соответствует требуемому типу TValue", nameof(key));
            }
        }

        void IDictionary.Remove(object key)
        {
            if (key is TKey typedKey)
                TryRemove(typedKey, out _);
            throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
        }

        [CanBeNull]
        object IDictionary.this[object key]
        {
            get
            {
                if (!(key is TKey typedKey))
                    throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
                return this[typedKey];
            }
            set
            {
                if (!(key is TKey typedKey))
                    throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
                switch (value)
                {
                    case TValue typedValue:
                        this[typedKey] = typedValue;
                        break;
                    case null:
                        this[typedKey] = default;
                        break;
                    default:
                        throw new ArgumentException("Тип параметра value не соответствует требуемому типу TValue", nameof(key));
                }
            }
        }

        /// <summary>
        ///     Очищает словарь.
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            _synchronizationContext.Post(_ =>
            {
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnPropertyChanged(nameof(Keys));
                OnPropertyChanged(nameof(Values));
            }, null);
        }

        void IDictionary<TKey, TValue>.Add(TKey key, [CanBeNull] TValue value) => TryAdd(key, value);

        bool IDictionary<TKey, TValue>.Remove(TKey key) => TryRemove(key, out _);

        [CanBeNull]
        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get => this[key];
            set => this[key] = value;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => TryAdd(item.Key, item.Value);

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => TryRemove(item.Key, out _);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)this).Contains(item);

        /// <summary>
        ///     Копирует элементы словаря в указанный массив, начиная с указанного индекса массива.
        /// </summary>
        /// <param name="array">Массив, в который копируются элементы словаря.</param>
        /// <param name="arrayIndex">Индекс в массиве, с которого начинается копирование.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue>>)this).CopyTo(array, arrayIndex);

        /// <summary>
        ///     Событие, возникающее при изменении коллекции.
        /// </summary>
        [CanBeNull]
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Событие, возникающее при изменении свойства.
        /// </summary>
        [CanBeNull]
        public event PropertyChangedEventHandler PropertyChanged;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => ((IReadOnlyDictionary<TKey, TValue>)this).Keys;

        [ItemCanBeNull]
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => ((IReadOnlyDictionary<TKey, TValue>)this).Values;

        /// <summary>
        ///     Получает или задает значение, связанное с указанным ключом.
        /// </summary>
        /// <param name="key">Ключ для получения или задания значения.</param>
        /// <returns>Значение, связанное с указанным ключом.</returns>
        [CanBeNull]
        public new TValue this[TKey key]
        {
            get => base[key];
            set
            {
                if (base[key]?.Equals(value) ?? value == null)
                    return;
                base[key] = value;
                _synchronizationContext.Post(_ =>
                {
                    OnPropertyChanged("Item[]");
                    OnPropertyChanged(nameof(Values));
                }, null);
            }
        }

        /// <summary>
        ///     Добавляет элемент с указанным ключом и значением в словарь.
        /// </summary>
        /// <param name="key">Ключ добавляемого элемента.</param>
        /// <param name="value">Значение добавляемого элемента.</param>
        /// <returns>Значение <c>true</c>, если элемент был успешно добавлен; иначе <c>false</c>.</returns>
        public new bool TryAdd(TKey key, [CanBeNull] TValue value)
        {
            if (!base.TryAdd(key, value))
                return false;
            _synchronizationContext.Post(_ =>
            {
                OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnPropertyChanged(nameof(Keys));
                OnPropertyChanged(nameof(Values));
            }, null);
            return true;
        }

        /// <summary>
        ///     Обновляет элемент с указанным ключом и значением в словаре, если текущее значение совпадает с указанным значением для сравнения.
        /// </summary>
        /// <param name="key">Ключ обновляемого элемента.</param>
        /// <param name="value">Новое значение для обновления.</param>
        /// <param name="comparisonValue">Значение для сравнения.</param>
        /// <returns>Значение <c>true</c>, если элемент был успешно обновлен; иначе <c>false</c>.</returns>
        public new bool TryUpdate(TKey key, [CanBeNull] TValue value, [CanBeNull] TValue comparisonValue)
        {
            if (!base.TryUpdate(key, value, comparisonValue))
                return false;
            _synchronizationContext.Post(_ =>
            {
                OnPropertyChanged("Item[]");
                OnPropertyChanged(nameof(Values));
            }, null);
            return true;
        }

        /// <summary>
        ///     Удаляет элемент с указанным ключом из словаря.
        /// </summary>
        /// <param name="key">Ключ удаляемого элемента.</param>
        /// <param name="value">Значение удаляемого элемента.</param>
        /// <returns>Значение <c>true</c>, если элемент был успешно удален; иначе <c>false</c>.</returns>
        public new bool TryRemove(TKey key, [CanBeNull] out TValue value)
        {
            if (!base.TryRemove(key, out value))
                return false;
            var returnValue = value;
            _synchronizationContext.Post(_ =>
            {
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, returnValue));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnPropertyChanged(nameof(Keys));
                OnPropertyChanged(nameof(Values));
            }, null);
            return true;
        }

        /// <summary>
        ///     Возвращает значение, связанное с указанным ключом, или добавляет новое значение, если ключ не найден.
        /// </summary>
        /// <param name="key">Ключ для поиска или добавления.</param>
        /// <param name="value">Значение для добавления, если ключ не найден.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public new TValue GetOrAdd(TKey key, [CanBeNull] TValue value) => TryGetValue(key, out var existingValue) ? existingValue : TryAdd(key, value) ? value : default;

        /// <summary>
        ///     Возвращает значение, связанное с указанным ключом, или добавляет новое значение, если ключ не найден, используя фабрику значений.
        /// </summary>
        /// <param name="key">Ключ для поиска или добавления.</param>
        /// <param name="valueFactory">Фабрика значений для добавления, если ключ не найден.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public new TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            if (TryGetValue(key, out var existingValue))
                return existingValue;
            var value = valueFactory(key);
            return TryAdd(key, value) ? value : default;
        }

        /// <summary>
        ///     Возвращает значение, связанное с указанным ключом, или добавляет новое значение, если ключ не найден, используя фабрику значений с аргументом.
        /// </summary>
        /// <param name="key">Ключ для поиска или добавления.</param>
        /// <param name="valueFactory">Фабрика значений для добавления, если ключ не найден.</param>
        /// <param name="factoryArgument">Аргумент для фабрики значений.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public new TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
        {
            if (TryGetValue(key, out var existingValue))
                return existingValue;
            var value = valueFactory(key, factoryArgument);
            return TryAdd(key, value) ? value : default;
        }

        /// <summary>
        ///     Добавляет или обновляет элемент с указанным ключом и значением в словарь, используя фабрики значений.
        /// </summary>
        /// <param name="key">Ключ добавляемого или обновляемого элемента.</param>
        /// <param name="addValueFactory">Фабрика значений для добавления.</param>
        /// <param name="updateValueFactory">Фабрика значений для обновления.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public new TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            while (true)
                if (TryGetValue(key, out var oldValue))
                {
                    var newValue = updateValueFactory(key, oldValue);
                    if (TryUpdate(key, newValue, oldValue))
                        return newValue;
                } else
                {
                    var addValue = addValueFactory(key);
                    if (TryAdd(key, addValue))
                        return addValue;
                }
        }

        /// <summary>
        ///     Добавляет или обновляет элемент с указанным ключом и значением в словарь, используя фабрики значений с аргументом.
        /// </summary>
        /// <param name="key">Ключ добавляемого или обновляемого элемента.</param>
        /// <param name="addValueFactory">Фабрика значений для добавления.</param>
        /// <param name="updateValueFactory">Фабрика значений для обновления.</param>
        /// <param name="factoryArgument">Аргумент для фабрики значений.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public new TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
        {
            while (true)
                if (TryGetValue(key, out var oldValue))
                {
                    var newValue = updateValueFactory(key, oldValue, factoryArgument);
                    if (TryUpdate(key, newValue, oldValue))
                        return newValue;
                } else
                {
                    var addValue = addValueFactory(key, factoryArgument);
                    if (TryAdd(key, addValue))
                        return addValue;
                }
        }

        /// <summary>
        ///     Добавляет или обновляет элемент с указанным ключом и значением в словарь, используя значение и фабрику значений.
        /// </summary>
        /// <param name="key">Ключ добавляемого или обновляемого элемента.</param>
        /// <param name="value">Значение для добавления или обновления.</param>
        /// <param name="updateValueFactory">Фабрика значений для обновления.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public new TValue AddOrUpdate(TKey key, TValue value, Func<TKey, TValue, TValue> updateValueFactory)
        {
            while (true)
                if (TryGetValue(key, out var oldValue))
                {
                    var newValue = updateValueFactory(key, oldValue);
                    if (TryUpdate(key, newValue, oldValue))
                        return newValue;
                } else if (TryAdd(key, value))
                    return value;
        }

        /// <summary>
        ///     Добавляет или обновляет элемент с указанным ключом и значением в словарь.
        /// </summary>
        /// <param name="key">Ключ добавляемого или обновляемого элемента.</param>
        /// <param name="value">Значение для добавления или обновления.</param>
        /// <returns>Значение, связанное с ключом, или добавленное значение.</returns>
        [CanBeNull]
        public TValue AddOrUpdate(TKey key, TValue value)
        {
            while (true)
                if (TryGetValue(key, out var oldValue))
                {
                    if (TryUpdate(key, value, oldValue))
                        return value;
                } else if (TryAdd(key, value))
                    return value;
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction notifyCollectionChangedAction, [CanBeNull] KeyValuePair<TKey, TValue>? changedItem = null) => CollectionChanged?.Invoke(this,
            (notifyCollectionChangedAction is NotifyCollectionChangedAction.Reset && changedItem is null) ||
            notifyCollectionChangedAction is NotifyCollectionChangedAction.Add ||
            notifyCollectionChangedAction is NotifyCollectionChangedAction.Remove
                ? new NotifyCollectionChangedEventArgs(notifyCollectionChangedAction, changedItem)
                : new NotifyCollectionChangedEventArgs(notifyCollectionChangedAction));

        private void OnPropertyChanged([CallerMemberName] [CanBeNull] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}