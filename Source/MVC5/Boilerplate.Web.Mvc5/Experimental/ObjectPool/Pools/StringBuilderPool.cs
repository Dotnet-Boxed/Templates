// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System.Text;

    /// <summary>
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis.Workspaces/Formatting/StringBuilderPool.cs,039ef0c630df07c3
    /// A pool of <see cref="StringBuilder"/> objects.
    /// </summary>
    public static class StringBuilderPool
    {
        /// <summary>
        /// Allocates an instance of <see cref="StringBuilder"/> from the object pool.
        /// </summary>
        /// <returns>An instance of <see cref="StringBuilder"/>.</returns>
        public static StringBuilder Allocate()
        {
            return SharedPools.Default<StringBuilder>().AllocateAndClear();
        }

        /// <summary>
        /// Frees the specified string builder back to the object pool.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public static void Free(StringBuilder stringBuilder)
        {
            SharedPools.Default<StringBuilder>().ClearAndFree(stringBuilder);
        }

        /// <summary>
        /// Returns the <see cref="string"/> representation of the <paramref name="stringBuilder"/> and frees it back 
        /// to the object pool.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <returns>The <see cref="string"/> representation of the <paramref name="stringBuilder"/>.</returns>
        public static string ReturnAndFree(StringBuilder stringBuilder)
        {
            SharedPools.Default<StringBuilder>().ForgetTrackedObject(stringBuilder);
            return stringBuilder.ToString();
        }
    }
}