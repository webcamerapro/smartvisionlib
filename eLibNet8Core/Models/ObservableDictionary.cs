using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace eLibNet8Core.Models;

/// <summary>
///     Представляет словарь, который поддерживает уведомления об изменениях коллекции.
/// </summary>
/// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
/// <typeparam name="TValue">Тип значений в словаре.</typeparam>
[SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
[Serializable]
public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue?>, IDictionary, IDictionary<TKey, TValue?>, IReadOnlyDictionary<TKey, TValue?> where TKey : notnull
{
    private readonly SynchronizationContext _synchronizationContext = AsyncOperationManager.SynchronizationContext;

    void IDictionary.Add(object key, object? value)
    {
        if (key is not TKey typedKey)
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
                throw new ArgumentException("Тип параметра value не соответствует требуемому типу TValue?", nameof(key));
        }
    }

    void IDictionary.Remove(object key)
    {
        if (key is TKey typedKey)
            Remove(typedKey, out _);
        throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
    }

    object? IDictionary.this[object key]
    {
        get
        {
            if (key is not TKey typedKey)
                throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
            return this[typedKey];
        }
        set
        {
            if (key is not TKey typedKey)
                throw new ArgumentException("Тип параметра key не соответствует требуемому типу TKey", nameof(key));
            this[typedKey] = value switch
            {
                TValue typedValue => typedValue,
                null              => default,
                _                 => throw new ArgumentException("Тип параметра value не соответствует требуемому типу TValue?", nameof(key))
            };
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

    /// <summary>
    ///     Добавляет элемент с указанным ключом и значением в словарь.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">Значение добавляемого элемента.</param>
    public new void Add(TKey key, TValue? value) => TryAdd(key, value);

    /// <summary>
    ///     Удаляет элемент с указанным ключом из словаря.
    /// </summary>
    /// <param name="key">Ключ удаляемого элемента.</param>
    /// <returns>Значение <c>true</c>, если элемент был успешно удален; иначе <c>false</c>.</returns>
    public new bool Remove(TKey key)
    {
        if (!base.Remove(key))
            return false;
        _synchronizationContext.Post(_ =>
        {
            OnCollectionChanged(NotifyCollectionChangedAction.Remove);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnPropertyChanged(nameof(Keys));
            OnPropertyChanged(nameof(Values));
        }, null);
        return true;
    }

    TValue? IDictionary<TKey, TValue?>.this[TKey key]
    {
        get => this[key];
        set => this[key] = value;
    }

    void ICollection<KeyValuePair<TKey, TValue?>>.Add(KeyValuePair<TKey, TValue?> item) => TryAdd(item.Key, item.Value);

    bool ICollection<KeyValuePair<TKey, TValue?>>.Remove(KeyValuePair<TKey, TValue?> item) => Remove(item.Key, out _);

    bool ICollection<KeyValuePair<TKey, TValue?>>.Contains(KeyValuePair<TKey, TValue?> item) => ((ICollection<KeyValuePair<TKey, TValue?>>)this).Contains(item);

    ICollection<TKey> IDictionary<TKey, TValue?>.Keys => Keys;

    ICollection<TValue?> IDictionary<TKey, TValue?>.Values => Values;

    /// <summary>
    ///     Копирует элементы словаря в указанный массив, начиная с указанного индекса массива.
    /// </summary>
    /// <param name="array">Массив, в который копируются элементы словаря.</param>
    /// <param name="arrayIndex">Индекс в массиве, с которого начинается копирование.</param>
    public void CopyTo(KeyValuePair<TKey, TValue?>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue?>>)this).CopyTo(array, arrayIndex);

    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue?>.Keys => ((IReadOnlyDictionary<TKey, TValue?>)this).Keys;

    IEnumerable<TValue?> IReadOnlyDictionary<TKey, TValue?>.Values => ((IReadOnlyDictionary<TKey, TValue?>)this).Values;

    /// <summary>
    ///     Получает или задает значение, связанное с указанным ключом.
    /// </summary>
    /// <param name="key">Ключ для получения или задания значения.</param>
    /// <returns>Значение, связанное с указанным ключом.</returns>
    public new TValue? this[TKey key]
    {
        get => base[key];
        set
        {
            if (base[key]?.Equals(value) ?? value is null)
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
    ///     Событие, возникающее при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    ///     Событие, возникающее при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Добавляет элемент с указанным ключом и значением в словарь.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">Значение добавляемого элемента.</param>
    /// <returns>Значение <c>true</c>, если элемент был успешно добавлен; иначе <c>false</c>.</returns>
    public new bool TryAdd(TKey key, TValue? value)
    {
        if (!base.TryAdd(key, value))
            return false;
        _synchronizationContext.Post(_ =>
        {
            OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue?>(key, value));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnPropertyChanged(nameof(Keys));
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
    public new bool Remove(TKey key, out TValue? value)
    {
        if (!base.Remove(key, out value))
            return false;
        var returnedValue = value;
        _synchronizationContext.Post(_ =>
        {
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue?>(key, returnedValue));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnPropertyChanged(nameof(Keys));
            OnPropertyChanged(nameof(Values));
        }, null);
        return true;
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction notifyCollectionChangedAction, KeyValuePair<TKey, TValue?>? changedItem = null) => CollectionChanged?.Invoke(this,
        (notifyCollectionChangedAction is NotifyCollectionChangedAction.Reset && changedItem is null) || notifyCollectionChangedAction is NotifyCollectionChangedAction.Add or NotifyCollectionChangedAction.Remove
            ? new(notifyCollectionChangedAction, changedItem)
            : new(notifyCollectionChangedAction));

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new(propertyName));
}