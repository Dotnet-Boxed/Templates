namespace Boilerplate.Web.Mvc.Caching
{
    using System;
    using System.IO;
#if NET451
    using System.Runtime.Serialization.Formatters.Binary;
#endif
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="IDistributedCache"/> extension methods.
    /// </summary>
    public static class DistributedCacheExtensions
    {
        #region BinaryReader & BinaryWriter

        public static async Task<bool> GetBooleanAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadBoolean();
            }
        }

        public static async Task<char> GetCharAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadChar();
            }
        }

        public static async Task<decimal> GetDecimalAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadDecimal();
            }
        }

        public static async Task<double> GetDoubleAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadDouble();
            }
        }

        public static async Task<short> GetShortAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadInt16();
            }
        }

        public static async Task<int> GetIntAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadInt32();
            }
        }

        public static async Task<long> GetLongAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadInt64();
            }
        }

        public static async Task<float> GetFloatAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadSingle();
            }
        }

        public static async Task<string> GetStringAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                return binaryReader.ReadString();
            }
        }

        public static Task SetAsync(this IDistributedCache cache, string key, bool value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, bool value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, char value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, char value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, decimal value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, decimal value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, double value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, double value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, short value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, short value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, int value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, int value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, long value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, long value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, float value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, float value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static Task SetAsync(this IDistributedCache cache, string key, string value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync(this IDistributedCache cache, string key, string value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(value);
                bytes = memoryStream.ToArray();
            }
            return cache.SetAsync(key, bytes, options);
        }

        public static async Task<Tuple<bool, bool>> TryGetBooleanAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, bool>(false, false);
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                bool value = binaryReader.ReadBoolean();
                return new Tuple<bool, bool>(true, value);
            }
        }

        public static async Task<Tuple<bool, char>> TryGetCharAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, char>(false, default(char));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadChar();
                return new Tuple<bool, char>(true, value);
            }
        }

        public static async Task<Tuple<bool, decimal>> TryGetDecimalAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, decimal>(false, default(decimal));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadDecimal();
                return new Tuple<bool, decimal>(true, value);
            }
        }

        public static async Task<Tuple<bool, double>> TryGetDoubleAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, double>(false, default(double));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadDouble();
                return new Tuple<bool, double>(true, value);
            }
        }

        public static async Task<Tuple<bool, short>> TryGetShortAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, short>(false, default(short));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadInt16();
                return new Tuple<bool, short>(true, value);
            }
        }

        public static async Task<Tuple<bool, int>> TryGetIntAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, int>(false, default(int));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadInt32();
                return new Tuple<bool, int>(true, value);
            }
        }

        public static async Task<Tuple<bool, long>> TryGetLongAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, long>(false, default(long));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadInt64();
                return new Tuple<bool, long>(true, value);
            }
        }

        public static async Task<Tuple<bool, float>> TryGetFloatAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, float>(false, default(long));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadSingle();
                return new Tuple<bool, float>(true, value);
            }
        }

        public static async Task<Tuple<bool, string>> TryGetStringAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, string>(false, default(string));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var value = binaryReader.ReadString();
                return new Tuple<bool, string>(true, value);
            }
        }

        #endregion

        #region JSON

        public static async Task<T> GetAsJsonAsync<T>(this IDistributedCache cache, string key)
        {
            var json = await GetStringAsync(cache, key);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static Task SetAsJsonAsync<T>(this IDistributedCache cache, string key, T value)
        {
            return SetAsJsonAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsJsonAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
        {
            var json = JsonConvert.SerializeObject(value, Formatting.None);
            return SetAsync(cache, key, json, options);
        }

        public static async Task<Tuple<bool, T>> TryGetAsJsonAsync<T>(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
            {
                return new Tuple<bool, T>(false, default(T));
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryReader = new BinaryReader(memoryStream);
                var json = await GetStringAsync(cache, key);
                var value = JsonConvert.DeserializeObject<T>(json);
                return new Tuple<bool, T>(true, value);
            }
        }

        #endregion

        #region BinaryFormatter
#if NET451
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                var binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, value);
                bytes = memoryStream.ToArray();
            }

            return cache.SetAsync(key, bytes, options);
        }
#endif
        #endregion
    }
}