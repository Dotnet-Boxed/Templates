// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    public sealed class ArrayBuilder<T>
    {
        #region Fields

        private static readonly ObjectPool<ArrayBuilder<T>> poolInstance = new ObjectPool<ArrayBuilder<T>>(() => new ArrayBuilder<T>(), 16);
        private static readonly ReadOnlyCollection<T> empty = new ReadOnlyCollection<T>(new T[0]);

        private readonly List<T> items;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ArrayBuilder{T}"/> class from being created.
        /// </summary>
        private ArrayBuilder()
        {
            this.items = new List<T>();
        }

        #endregion

        #region Public Static Methods

        public static ArrayBuilder<T> GetInstance(int size = 0)
        {
            ArrayBuilder<T> arrayBuilder = poolInstance.Allocate();

            Debug.Assert(arrayBuilder.Count == 0);

            if (size > 0)
            {
                arrayBuilder.items.Capacity = size;
            }

            return arrayBuilder;
        }

        #endregion

        #region Public Properties

        public T this[int i]
        {
            get { return this.items[i]; }
        }

        public int Count
        {
            get { return this.items.Count; }
        } 

        #endregion

        public void Add(T item)
        {
            this.items.Add(item);
        }

        public void AddRange(T[] items)
        {
            foreach (T item in items)
            {
                this.items.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            this.items.AddRange(items);
        }

        public T Peek()
        {
            return this.items[this.items.Count - 1];
        }
        
        public void Push(T item)
        {
            this.Add(item);
        }

        public T Pop()
        {
            int position = this.items.Count - 1;
            T result = this.items[position];
            this.items.RemoveAt(position);
            return result;
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public void Free()
        {
            this.items.Clear();
            poolInstance.Free(this);
        }

        public T[] ToArray()
        {
            return this.items.ToArray();
        }

        public T[] ToArrayAndFree()
        {
            T[] result = this.ToArray();
            this.Free();
            return result;
        }

        public ReadOnlyCollection<T> ToImmutable()
        {
            return (this.items.Count > 0) ? new ReadOnlyCollection<T>(this.items.ToArray()) : empty;
        }

        public ReadOnlyCollection<T> ToImmutableAndFree()
        {
            ReadOnlyCollection<T> result = this.ToImmutable();
            this.Free();
            return result;
        }

        public void Sort(IComparer<T> comparer)
        {
            this.items.Sort(comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}
