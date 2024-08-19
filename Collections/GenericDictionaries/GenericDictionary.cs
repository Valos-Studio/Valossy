using System.Collections.Generic;

namespace Valossy.Collections.GenericDictionaries;

public partial class GenericDictionary<TKey, TValue>
    where TValue : class
{
    public TValue this[TKey key]
    {
        get => this.dictionary[key] as TValue;
        set => this.dictionary[key] = value;
    }

    public ICollection<TKey> Keys { get; }
    public ICollection<TValue> Values { get; }
    public bool IsReadOnly { get; }
    public int Count { get; }

    private readonly Dictionary<TKey, object> dictionary;

    public GenericDictionary()
    {
        this.dictionary = new Dictionary<TKey, object>();
    }

    public GenericDictionary(int capacity)
    {
        this.dictionary = new Dictionary<TKey, object>(capacity);
    }

    public IEnumerator<KeyValuePair<TKey, object>> GetEnumerator()
    {
        return this.dictionary.GetEnumerator();
    }

    public bool ContainsKey(TKey key)
    {
        return this.dictionary.ContainsKey(key);
    }
    
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }
    
    public void Add(TKey key, TValue value)
    {
        this.dictionary.Add(key, value);
    }
    
    public bool TryGetValue(TKey key, out TValue value)
    {
        bool result = this.dictionary.TryGetValue(key, out object returnValue);
        
        value = (TValue)returnValue;

        return result;
    }
    
    public bool Remove(TKey key)
    {
        return this.dictionary.Remove(key);
    }

    public void Clear()
    {
        this.dictionary.Clear();
    }
}