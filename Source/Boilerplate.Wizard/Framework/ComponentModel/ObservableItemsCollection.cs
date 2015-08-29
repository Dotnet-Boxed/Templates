namespace Framework.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> which is also capable of tracking when an item has changed.
    /// </summary>
    /// <typeparam name="T">The type of the items within this instance.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    public class ObservableItemsCollection<T> : ObservableCollection<T>
    {
        public event EventHandler<ItemChangedEventArgs<T>> ItemChanged;

        #region Constructors

        public ObservableItemsCollection()
        {
        }

        public ObservableItemsCollection(IEnumerable<T> items)
        {
            this.AddRange(items);
        } 

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the elements of the specified collection to this instance.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (T item in collection)
                {
                    this.Add(item);
                }
            }
        }
        
        /// <summary>
        /// Returns a read-only <see cref="ReadOnlyObservableCollection{T}"/> wrapper for the current
        /// collection.
        /// </summary>
        /// <returns>A <see cref="ReadOnlyObservableCollection{T}"/> that acts as a read-only
        /// wrapper around the current collection.</returns>
        public ReadOnlyObservableCollection<T> AsReadOnly()
        {
            return new ReadOnlyObservableCollection<T>(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (T item in this.Items)
            {
                this.UnregisterPropertyChangedEvent(item);
            }

            base.ClearItems();
        }

        /// <summary>
        /// Inserts an item into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            this.RegisterPropertyChangedEvent(item);
        }

        /// <summary>
        /// Called when an item in the collection has changed.
        /// </summary>
        /// <param name="e">The item changed event arguments.</param>
        protected virtual void OnItemChanged(ItemChangedEventArgs<T> e)
        {
            this.ItemChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Removes the item at the specified index of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
        {
            T item = this.Items[index];
            this.UnregisterPropertyChangedEvent(item);

            base.RemoveItem(index);
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index.</param>
        protected override void SetItem(int index, T item)
        {
            T oldItem = this.Items[index];
            this.UnregisterPropertyChangedEvent(oldItem);

            base.SetItem(index, item);

            this.RegisterPropertyChangedEvent(item);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registers the property changed event.
        /// </summary>
        /// <param name="item">The item.</param>
        private void RegisterPropertyChangedEvent(T item)
        {
            INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
            {
                notifyPropertyChanged.PropertyChanged += this.OnItemPropertyChanged;
            }
        }

        /// <summary>
        /// Unregisters the property changed event.
        /// </summary>
        /// <param name="item">The item.</param>
        private void UnregisterPropertyChangedEvent(T item)
        {
            INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
            {
                notifyPropertyChanged.PropertyChanged -= this.OnItemPropertyChanged;
            }
        }

        /// <summary>
        /// Called when the property in an item in this instance changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnItemChanged(new ItemChangedEventArgs<T>((T)sender, e.PropertyName));
        }

        #endregion
    }
}
