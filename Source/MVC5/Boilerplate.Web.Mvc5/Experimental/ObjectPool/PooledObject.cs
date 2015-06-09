namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis.Workspaces/Utilities/ObjectPools/PooledObject.cs,b2e28c15ee358f81
    /// This is resource acquisition is initialization (RAII) object to automatically release pooled object when its owning pool.
    /// </summary>
    public struct PooledObject<T> : IDisposable where T : class
    {
        private readonly Action<ObjectPool<T>, T> releaser;
        private readonly ObjectPool<T> pool;
        private T pooledObject;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledObject{T}"/> struct.
        /// </summary>
        /// <param name="pool">The object pool.</param>
        /// <param name="allocator">The allocator function that allocates a new instance of <typeparamref name="T"/>.</param>
        /// <param name="releaser">The releaser function that de-allocates an instance of <typeparamref name="T"/>.</param>
        public PooledObject(
            ObjectPool<T> pool, 
            Func<ObjectPool<T>, T> allocator, 
            Action<ObjectPool<T>, T> releaser) 
            : this()
        {
            this.pool = pool;
            this.pooledObject = allocator(pool);
            this.releaser = releaser;
        }

        #endregion

        /// <summary>
        /// Gets the pooled object.
        /// </summary>
        /// <value>
        /// The pooled object.
        /// </value>
        public T Object
        {
            get { return this.pooledObject; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.pooledObject != null)
            {
                this.releaser(this.pool, this.pooledObject);
                this.pooledObject = null;
            }
        }

        #region Public Static Methods

        public static PooledObject<StringBuilder> Create(ObjectPool<StringBuilder> pool)
        {
            return new PooledObject<StringBuilder>(pool, Allocator, Releaser);
        }

        public static PooledObject<Stack<TItem>> Create<TItem>(ObjectPool<Stack<TItem>> pool)
        {
            return new PooledObject<Stack<TItem>>(pool, Allocator, Releaser);
        }

        public static PooledObject<Queue<TItem>> Create<TItem>(ObjectPool<Queue<TItem>> pool)
        {
            return new PooledObject<Queue<TItem>>(pool, Allocator, Releaser);
        }

        public static PooledObject<HashSet<TItem>> Create<TItem>(ObjectPool<HashSet<TItem>> pool)
        {
            return new PooledObject<HashSet<TItem>>(pool, Allocator, Releaser);
        }

        public static PooledObject<Dictionary<TKey, TValue>> Create<TKey, TValue>(ObjectPool<Dictionary<TKey, TValue>> pool)
        {
            return new PooledObject<Dictionary<TKey, TValue>>(pool, Allocator, Releaser);
        }

        public static PooledObject<List<TItem>> Create<TItem>(ObjectPool<List<TItem>> pool)
        {
            return new PooledObject<List<TItem>>(pool, Allocator, Releaser);
        }

        #endregion

        #region Private Static Methods

        private static StringBuilder Allocator(ObjectPool<StringBuilder> pool)
        {
            return pool.AllocateAndClear();
        }

        private static void Releaser(ObjectPool<StringBuilder> pool, StringBuilder stringBuilder)
        {
            pool.ClearAndFree(stringBuilder);
        }

        private static Stack<TItem> Allocator<TItem>(ObjectPool<Stack<TItem>> pool)
        {
            return pool.AllocateAndClear();
        }

        private static void Releaser<TItem>(ObjectPool<Stack<TItem>> pool, Stack<TItem> stack)
        {
            pool.ClearAndFree(stack);
        }

        private static Queue<TItem> Allocator<TItem>(ObjectPool<Queue<TItem>> pool)
        {
            return pool.AllocateAndClear();
        }

        private static void Releaser<TItem>(ObjectPool<Queue<TItem>> pool, Queue<TItem> queue)
        {
            pool.ClearAndFree(queue);
        }

        private static HashSet<TItem> Allocator<TItem>(ObjectPool<HashSet<TItem>> pool)
        {
            return pool.AllocateAndClear();
        }

        private static void Releaser<TItem>(ObjectPool<HashSet<TItem>> pool, HashSet<TItem> hashSet)
        {
            pool.ClearAndFree(hashSet);
        }

        private static Dictionary<TKey, TValue> Allocator<TKey, TValue>(ObjectPool<Dictionary<TKey, TValue>> pool)
        {
            return pool.AllocateAndClear();
        }

        private static void Releaser<TKey, TValue>(ObjectPool<Dictionary<TKey, TValue>> pool, Dictionary<TKey, TValue> dictionary)
        {
            pool.ClearAndFree(dictionary);
        }

        private static List<TItem> Allocator<TItem>(ObjectPool<List<TItem>> pool)
        {
            return pool.AllocateAndClear();
        }

        private static void Releaser<TItem>(ObjectPool<List<TItem>> pool, List<TItem> list)
        {
            pool.ClearAndFree(list);
        }

        #endregion
    }
}