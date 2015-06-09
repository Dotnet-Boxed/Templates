// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in 
// the project root for license information.

// define TRACE_LEAKS to get additional diagnostics that can lead to the leak sources. note: it will
// make everything about 2-3x slower
// 
// #define TRACE_LEAKS

// define DETECT_LEAKS to detect possible leaks
// #if DEBUG
// #define DETECT_LEAKS  //for now always enable DETECT_LEAKS in debug.
// #endif
namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System;
    using System.Diagnostics;
    using System.Threading;
#if DETECT_LEAKS
using System.Runtime.CompilerServices;
#endif

    /// <summary>
    /// Generic implementation of object pooling pattern with predefined pool size limit. The main purpose is that 
    /// limited number of frequently used objects can be kept in the pool for further recycling.
    /// 
    /// Notes: 
    /// 1) it is not the goal to keep all returned objects. Pool is not meant for storage. If there is no space in the 
    /// pool, extra returned objects will be dropped.
    /// 
    /// 2) it is implied that if object was obtained from a pool, the caller will return it back in a relatively short 
    /// time. Keeping checked out objects for long durations is ok, but reduces usefulness of pooling. Just new up your 
    /// own.
    /// 
    /// Not returning objects to the pool in not detrimental to the pool's work, but is a bad practice. 
    /// Rationale: 
    /// If there is no intent for reusing the object, do not use pool - just use "new". 
    /// 
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis/ObjectPool%25601.cs,20b9a041fb2d5b00
    /// </summary>
    public class ObjectPool<T> where T : class
    {
        private struct Element
        {
            internal T Value;
        }

        /// <summary>
        /// The first item is stored in a dedicated field because we expect to be able to satisfy most requests from it.
        /// </summary>
        private T firstItem;

        /// <summary>
        /// Storage for the pool objects.
        /// </summary>
        private readonly Element[] items;

        /// <summary>
        /// Factory is stored for the lifetime of the pool. We will call this only when pool needs to
        /// expand. compared to "new T()", Func gives more flexibility to implementers and faster
        /// than "new T()".
        /// </summary>
        private readonly Func<T> factory;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ObjectPool(Func<T> factory)
            : this(factory, Environment.ProcessorCount * 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="size">The size of the pool.</param>
        public ObjectPool(Func<T> factory, int size)
        {
            Debug.Assert(size >= 1);
            this.factory = factory;
            this.items = new Element[size - 1];
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Produces an instance.
        /// </summary>
        /// <remarks>
        /// Search strategy is a simple linear probing which is chosen for it cache-friendliness. Note that Free will 
        /// try to store recycled objects close to the start thus statistically reducing how far we will typically 
        /// search.
        /// </remarks>
        public T Allocate()
        {
            // PERF: Examine the first element. If that fails, AllocateSlow will look at the remaining elements.
            // Note that the initial read is optimistically not synchronized. That is intentional. 
            // We will interlock only when we have a candidate. in a worst case we may miss some
            // recently returned objects. Not a big deal.
            T instance = this.firstItem;
            if ((instance == null) || (instance != Interlocked.CompareExchange(ref this.firstItem, null, instance)))
            {
                instance = AllocateSlow();
            }

#if DETECT_LEAKS
            var tracker = new LeakTracker();
            leakTrackers.Add(instance, tracker);
 
#if TRACE_LEAKS
            var frame = CaptureStackTrace();
            tracker.Trace = frame;
#endif
#endif
            return instance;
        }

        /// <summary>
        /// Returns objects to the pool.
        /// </summary>
        /// <remarks>
        /// Search strategy is a simple linear probing which is chosen for it cache-friendliness.
        /// Note that Free will try to store recycled objects close to the start thus statistically 
        /// reducing how far we will typically search in Allocate.
        /// </remarks>
        public void Free(T obj)
        {
            this.Validate(obj);
            this.ForgetTrackedObject(obj);

            if (this.firstItem == null)
            {
                // Intentionally not using interlocked here. 
                // In a worst case scenario two objects may be stored into same slot.
                // It is very unlikely to happen and will only mean that one of the objects will get collected.
                this.firstItem = obj;
            }
            else
            {
                this.FreeSlow(obj);
            }
        }

        /// <summary>
        /// Removes an object from leak tracking.  
        /// 
        /// This is called when an object is returned to the pool.  It may also be explicitly 
        /// called if an object allocated from the pool is intentionally not being returned
        /// to the pool.  This can be of use with pooled arrays if the consumer wants to 
        /// return a larger array to the pool than was originally allocated.
        /// </summary>
        [Conditional("DEBUG")]
        internal void ForgetTrackedObject(T old, T replacement = null)
        {
#if DETECT_LEAKS
            LeakTracker tracker;
            if (leakTrackers.TryGetValue(old, out tracker))
            {
                tracker.Dispose();
                leakTrackers.Remove(old);
            }
            else
            {
                var trace = CaptureStackTrace();
                Debug.WriteLine($"TRACEOBJECTPOOLLEAKS_BEGIN\nObject of type {typeof(T)} was freed, but was not from pool. \n Callstack: \n {trace} TRACEOBJECTPOOLLEAKS_END");
            }
 
            if (replacement != null)
            {
                tracker = new LeakTracker();
                leakTrackers.Add(replacement, tracker);
            }
#endif
        } 

        #endregion

        #region Private Methods

        /// <summary>
        /// Allocates an instance if one already exists and is available, otherwise creates a new instance.
        /// </summary>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        private T AllocateSlow()
        {
            var items = this.items;

            for (int i = 0; i < items.Length; ++i)
            {
                // Note that the initial read is optimistically not synchronized. That is intentional. 
                // We will interlock only when we have a candidate. in a worst case we may miss some
                // recently returned objects. Not a big deal.
                T inst = items[i].Value;
                if (inst != null)
                {
                    if (inst == Interlocked.CompareExchange(ref items[i].Value, null, inst))
                    {
                        return inst;
                    }
                }
            }

            return this.CreateInstance();
        }

        /// <summary>
        /// Creates a new instance of <typeparamref name="T"/> from the factory.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="T"/>.</returns>
        private T CreateInstance()
        {
            var instance = this.factory();
            return instance;
        }

        /// <summary>
        /// Frees an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="obj">The free instance of type <typeparamref name="T"/>.</param>
        private void FreeSlow(T obj)
        {
            var items = this.items;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Value == null)
                {
                    // Intentionally not using interlocked here. 
                    // In a worst case scenario two objects may be stored into same slot.
                    // It is very unlikely to happen and will only mean that one of the objects will get collected.
                    items[i].Value = obj;
                    break;
                }
            }
        }

        [Conditional("DEBUG")]
        private void Validate(object obj)
        {
            Debug.Assert(obj != null, "freeing null?");

            var items = this.items;
            for (int i = 0; i < items.Length; i++)
            {
                var value = items[i].Value;
                if (value == null)
                {
                    return;
                }

                Debug.Assert(value != obj, "freeing twice?");
            }
        }

        #endregion

        #region Detect Leaks

#if DETECT_LEAKS
        private static readonly ConditionalWeakTable<T, LeakTracker> leakTrackers = new ConditionalWeakTable<T, LeakTracker>();
 
        private class LeakTracker : IDisposable
        {
            private volatile bool disposed;
 
#if TRACE_LEAKS
            internal volatile object Trace = null;
#endif

            public void Dispose()
            {
                disposed = true;
                GC.SuppressFinalize(this);
            }
 
            private string GetTrace()
            {
#if TRACE_LEAKS
                return Trace == null ? "" : Trace.ToString();
#else
                return "Leak tracing information is disabled. Define TRACE_LEAKS on ObjectPool`1.cs to get more info \n";
#endif
            }
 
            ~LeakTracker()
            {
                if (!this.disposed && !Environment.HasShutdownStarted)
                {
                    var trace = GetTrace();
 
                    // If you are seeing this message it means that object has been allocated from the pool 
                    // and has not been returned back. This is not critical, but turns pool into rather 
                    // inefficient kind of "new".
                    Debug.WriteLine($"TRACEOBJECTPOOLLEAKS_BEGIN\nPool detected potential leaking of {typeof(T)}. \n Location of the leak: \n {GetTrace()} TRACEOBJECTPOOLLEAKS_END");
                }
            }
        }
#endif

#if DETECT_LEAKS
        private static Lazy<Type> stackTraceType = new Lazy<Type>(() => Type.GetType("System.Diagnostics.StackTrace"));
 
        private static object CaptureStackTrace()
        {
            return Activator.CreateInstance(stackTraceType.Value);
        }
#endif

        #endregion
    }
}