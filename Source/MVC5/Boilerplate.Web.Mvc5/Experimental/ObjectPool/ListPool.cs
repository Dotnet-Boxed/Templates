// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System.Collections.Generic;

    /// <summary>
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis.Workspaces/Formatting/ListPool.cs,1086fa28bcfcb8ca
    /// </summary>
    internal static class ListPool<T>
    {
        public static List<T> Allocate()
        {
            return SharedPools.Default<List<T>>().AllocateAndClear();
        }

        public static void Free(List<T> list)
        {
            SharedPools.Default<List<T>>().ClearAndFree(list);
        }

        public static List<T> ReturnAndFree(List<T> list)
        {
            SharedPools.Default<List<T>>().ForgetTrackedObject(list);
            return list;
        }
    }
}