namespace Boilerplate.AspNetCore.Caching
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="IDistributedCache"/> extension methods.
    /// </summary>
    public static class DistributedCacheExtensions
    {
        /// <summary>
        /// Gets the <see cref="bool"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="bool"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<bool?> GetBooleanAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadBoolean();
            }
        }

        /// <summary>
        /// Gets the <see cref="char"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="char"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<char?> GetCharAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadChar();
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="decimal"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<decimal?> GetDecimalAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadDecimal();
            }
        }

        /// <summary>
        /// Gets the <see cref="double"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="double"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<double?> GetDoubleAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadDouble();
            }
        }

        /// <summary>
        /// Gets the <see cref="short"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="short"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<short?> GetShortAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadInt16();
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="int"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<int?> GetIntAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadInt32();
            }
        }

        /// <summary>
        /// Gets the <see cref="long"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="long"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<long?> GetLongAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadInt64();
            }
        }

        /// <summary>
        /// Gets the <see cref="float"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <returns>The <see cref="float"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<float?> GetFloatAsync(this IDistributedCache cache, string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> value with the specified key from the cache asynchronously or returns
        /// <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="encoding">The encoding of the <see cref="string"/> value or <c>null</c> to use UTF-8.</param>
        /// <returns>The <see cref="string"/> value or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<string> GetStringAsync(this IDistributedCache cache, string key, Encoding encoding = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var bytes = await cache.GetAsync(key).ConfigureAwait(false);
            if (bytes == null)
            {
                return null;
            }

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Gets the value of type <typeparamref name="T"/> with the specified key from the cache asynchronously by
        /// deserializing it from JSON format or returns <c>null</c> if the key was not found.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="encoding">The encoding of the JSON or <c>null</c> to use UTF-8.</param>
        /// <returns>The value of type <typeparamref name="T"/> or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static async Task<T> GetAsJsonAsync<T>(this IDistributedCache cache, string key, Encoding encoding = null)
            where T : class
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var json = await GetStringAsync(cache, key, encoding).ConfigureAwait(false);
            if (json == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Sets the <see cref="bool"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            bool value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="char"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            char value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="decimal"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            decimal value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="double"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            double value,
            DistributedCacheEntryOptions options)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="short"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            short value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="int"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            int value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="long"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            long value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="float"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            float value,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the <see cref="string"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            string value,
            DistributedCacheEntryOptions options = null)
        {
            return SetAsync(cache, key, value, null, options);
        }

        /// <summary>
        /// Sets the <see cref="string"/> value with the specified key in the cache asynchronously.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="encoding">The <see cref="string"/> values encoding or <c>null</c> for UTF-8.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>A task representing this action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsync(
            this IDistributedCache cache,
            string key,
            string value,
            Encoding encoding = null,
            DistributedCacheEntryOptions options = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            byte[] bytes = encoding.GetBytes(value);
            return cache.SetAsync(key, bytes, options);
        }

        /// <summary>
        /// Sets the value of type <typeparamref name="T"/> with the specified key in the cache asynchronously by
        /// serializing it to JSON format.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>The value of type <typeparamref name="T"/> or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsJsonAsync<T>(
            this IDistributedCache cache,
            string key,
            T value,
            DistributedCacheEntryOptions options = null)
            where T : class
        {
            return SetAsJsonAsync<T>(cache, key, value, null, options);
        }

        /// <summary>
        /// Sets the value of type <typeparamref name="T"/> with the specified key in the cache asynchronously by
        /// serializing it to JSON format.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="value">The cache item value.</param>
        /// <param name="encoding">The encoding to use for the JSON or <c>null</c> to use UTF-8.</param>
        /// <param name="options">The cache options or <c>null</c> to use the default cache options.</param>
        /// <returns>The value of type <typeparamref name="T"/> or <c>null</c> if the key was not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="key"/> is <c>null</c>.</exception>
        public static Task SetAsJsonAsync<T>(
            this IDistributedCache cache,
            string key,
            T value,
            Encoding encoding = null,
            DistributedCacheEntryOptions options = null)
            where T : class
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            var json = JsonConvert.SerializeObject(value, Formatting.None);
            return SetAsync(cache, key, json, encoding, options);
        }
    }
}