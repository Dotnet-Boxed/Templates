namespace MvcBoilerplate.Services
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A cached collection of key value pairs.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets the total number of cache entries in the cache.
        /// </summary>
        long Count { get; }

        /// <summary>
        /// Inserts a cache entry into the cache by using a key, a value and no expiration.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">The data for the cache entry.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        void AddOrUpdate<T>(
            string key,
            T value,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Inserts a cache entry into the cache by using a key, a value and absolute expiration.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">The data for the cache entry.</param>
        /// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        void AddOrUpdate<T>(
            string key,
            T value,
            DateTimeOffset absoluteExpiration,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Inserts a cache entry into the cache by using a key, a value and sliding expiration.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">The data for the cache entry.</param>
        /// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        void AddOrUpdate<T>(
            string key,
            T value,
            TimeSpan slidingExpiration,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Clears all items from the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether a cache entry exists in the cache with the specified key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry.</param>
        /// <returns><c>true</c> if a cache entry exists, otherwise <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        bool Contains(string key);

        /// <summary>
        /// Gets an entry from the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry to get.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to get.</param>
        /// <returns>A reference to the cache entry that is identified by key, if the entry exists; otherwise, <c>null</c>.</returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
        /// cache entry into the cache by using a key, a value and no expiration and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">The data for the cache entry.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        /// <returns>A reference to the cache entry that is identified by key.</returns>
        T GetOrAdd<T>(
            string key,
            Func<T> value,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
        /// cache entry into the cache by using a key, a value and absolute expiration and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">The data for the cache entry.</param>
        /// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        /// <returns>A reference to the cache entry that is identified by key.</returns>
        T GetOrAdd<T>(
            string key,
            Func<T> value,
            DateTimeOffset absoluteExpiration,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
        /// cache entry into the cache by using a key, a value and sliding expiration and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">The data for the cache entry.</param>
        /// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        /// <returns>A reference to the cache entry that is identified by key.</returns>
        T GetOrAdd<T>(
            string key,
            Func<T> value,
            TimeSpan slidingExpiration,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
        /// cache entry into the cache by using a key, a value and no expiration and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">A function that asynchronously gets the data for the cache entry.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        /// <returns>A reference to the cache entry that is identified by key.</returns>
        Task<T> GetOrAddAsync<T>(
            string key,
            Func<Task<T>> value,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
        /// cache entry into the cache by using a key, a value and absolute expiration and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">A function that asynchronously gets the data for the cache entry.</param>
        /// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        /// <returns>A reference to the cache entry that is identified by key.</returns>
        Task<T> GetOrAddAsync<T>(
            string key,
            Func<Task<T>> value,
            DateTimeOffset absoluteExpiration,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
        /// cache entry into the cache by using a key, a value and sliding expiration and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cache entry value.</typeparam>
        /// <param name="key">A unique identifier for the cache entry to insert.</param>
        /// <param name="value">A function that asynchronously gets the data for the cache entry.</param>
        /// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
        /// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
        /// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
        /// <returns>A reference to the cache entry that is identified by key.</returns>
        Task<T> GetOrAddAsync<T>(
            string key,
            Func<Task<T>> value,
            TimeSpan slidingExpiration,
            Action<string, T> afterItemRemoved = null,
            Action<string, T> beforeItemRemoved = null) where T : class;

        /// <summary>
        /// Removes a cache entry from the cache with the specified key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        void Remove(string key);
    }
}
