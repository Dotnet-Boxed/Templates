// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis.Workspaces/Utilities/ObjectPools/SharedPools.cs,b2114905209e7df3
    /// 
    /// Use this shared pool if only concern is reducing object allocations.
    /// if perf of an object pool itself is also a concern, use ObjectPool directly.
    /// 
    /// For example, if you want to create a million of small objects within a second, 
    /// use the ObjectPool directly. it should have much less overhead than using this.
    /// </summary>
    public static class SharedPools
    {
        /// pooled memory : 4K * 512 = 4MB
        public const int ByteBufferSize = 4 * 1024;
        private const int ByteBufferCount = 512;

        #region Public Static Methods

        /// <summary>
        /// Pool that uses default constructor with 100 elements pooled.
        /// </summary>
        public static ObjectPool<T> BigDefault<T>() where T : class, new()
        {
            List<object> list = SharedPools.Default<List<object>>().AllocateAndClear();

            SharedPools.Default<List<object>>().Free(list);

            return DefaultBigPool<T>.Instance;
        }

        /// <summary>
        /// Pool that uses default constructor with 20 elements pooled.
        /// </summary>
        public static ObjectPool<T> Default<T>() where T : class, new()
        {
            return DefaultNormalPool<T>.Instance;
        }

        /// <summary>
        /// Pool that uses string as key with StringComparer.OrdinalIgnoreCase as key comparer with 20 elements pooled.
        /// </summary>
        public static ObjectPool<Dictionary<string, T>> StringIgnoreCaseDictionary<T>()
        {
            return StringIgnoreCaseDictionaryNormalPool<T>.Instance;
        }

        /// <summary>
        /// Pool that uses string as element with StringComparer.OrdinalIgnoreCase as element comparer.
        /// </summary>
        public static readonly ObjectPool<HashSet<string>> StringIgnoreCaseHashSet = new ObjectPool<HashSet<string>>(
            () => new HashSet<string>(StringComparer.OrdinalIgnoreCase), 
            20);

        /// <summary>
        /// Pool that uses string as element with StringComparer.Ordinal as element comparer.
        /// </summary>
        public static readonly ObjectPool<HashSet<string>> StringHashSet = new ObjectPool<HashSet<string>>(
            () => new HashSet<string>(StringComparer.Ordinal), 
            20);

        /// <summary>
        /// Used to reduce the # of temporary byte[]s created to satisfy serialization and other I/O requests.
        /// </summary>
        public static readonly ObjectPool<byte[]> ByteArray = new ObjectPool<byte[]>(
            () => new byte[ByteBufferSize], 
            ByteBufferCount); 

        #endregion

        #region Private Static Classes

        /// <summary>
        /// A pool of objects of type <typeparamref name="T"/>, up to a maximum of 100 instances.
        /// </summary>
        /// <typeparam name="T">The type of the object to pool.</typeparam>
        private static class DefaultBigPool<T> where T : class, new()
        {
            /// <summary>
            /// The pool of objects of type <typeparamref name="T"/>, up to a maximum of 100 instances.
            /// </summary>
            public static readonly ObjectPool<T> Instance = new ObjectPool<T>(() => new T(), 100);
        }

        /// <summary>
        /// A pool of objects of type <typeparamref name="T"/>, up to a maximum of 20 instances.
        /// </summary>
        /// <typeparam name="T">The type of the object to pool.</typeparam>
        private static class DefaultNormalPool<T> where T : class, new()
        {
            /// <summary>
            /// The pool of objects of type <typeparamref name="T"/>, up to a maximum of 20 instances.
            /// </summary>
            public static readonly ObjectPool<T> Instance = new ObjectPool<T>(() => new T(), 20);
        }

        /// <summary>
        /// A pool of dictionaries of type <see cref="Dictionary{TKey, TValue}"/> with StringComparer.OrdinalIgnoreCase as key comparer, up to a maximum of 20 instances.
        /// </summary>
        /// <typeparam name="T">The type of the values in the dictionary.</typeparam>
        private static class StringIgnoreCaseDictionaryNormalPool<T>
        {
            /// <summary>
            /// The pool of dictionaries of type <see cref="Dictionary{TKey, TValue}"/> with StringComparer.OrdinalIgnoreCase as key comparer, up to a maximum of 20 instances.
            /// </summary>
            public static readonly ObjectPool<Dictionary<string, T>> Instance = new ObjectPool<Dictionary<string, T>>(
                () => new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase), 
                20);
        } 

        #endregion
    }
}