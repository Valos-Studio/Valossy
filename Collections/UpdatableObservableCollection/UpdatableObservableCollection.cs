using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Valossy.Controls.Generals;

namespace Valossy.Collections.UpdatableObservableCollection;

public class UpdatableObservableCollection<T> : ObservableCollection<T>, IListenToSelected, IBindingCollection
    where T : IHaveModelKey
{
    public delegate void ItemAddedEventHandler(T newItem);
    public delegate void ItemRemovedEventHandler(T removedItem);

    public event ItemAddedEventHandler ItemAdded;
    public event ItemRemovedEventHandler ItemRemoved;
    
    private readonly Dictionary<object, int> keyToIndex = new Dictionary<object, int>();

    private readonly object lockObject = new object();

    public T SelectedItem { get; set; }

    public new void Add(T item)
    {
        if (item == null)
        {
            return;
        }

        lock (this.lockObject)
        {
            if (this.keyToIndex.TryGetValue(item.ModelKey, out int index))
            {
                this.SetItem(index, item);
            }
            else
            {
                index = this.Count;
                this.InsertItem(index, item);
                this.keyToIndex[item.ModelKey] = index;
            }
            
            ItemAdded?.Invoke(item);
        }
    }

    public T Get(object key)
    {
        lock (this.lockObject)
        {
            this.keyToIndex.TryGetValue(key, out var index);
            return this[index];
        }
    }

    public List<object> GetItemsSafe()
    {
        lock (this.lockObject)
        {
            return this.Items.Select(x => x as object).ToList();
        }
    }

    public new void Remove(T item)
    {
        if (item == null)
        {
            return;
        }

        lock (this.lockObject)
        {
            if (this.keyToIndex.TryGetValue(item.ModelKey, out int index))
            {
                this.RemoveItem(index);
                this.keyToIndex.Remove(item.ModelKey);
                
                ItemRemoved?.Invoke(item);
            }
        }
    }

    public void RemoveSelected()
    {
        this.Remove(this.SelectedItem);
    }

    public void SelectedItemChanged(object sender, EventArgs e)
    {
        if (sender is T item)
        {
            this.SelectedItem = item;
        }
    }
}