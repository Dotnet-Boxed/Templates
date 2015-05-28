// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
namespace Boilerplate.Web.Mvc.Experimental.ObjectPool
{
    using System.Text;

    // TODO Comment http://mattwarren.org/2014/06/10/roslyn-code-base-performance-lessons-part-2/

    /// <summary>
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis.Workspaces/Formatting/StringBuilderPool.cs,039ef0c630df07c3
    /// </summary>
    internal static class StringBuilderPool
    {
        public static StringBuilder Allocate()
        {
            return SharedPools.Default<StringBuilder>().AllocateAndClear();
        }

        public static void Free(StringBuilder builder)
        {
            SharedPools.Default<StringBuilder>().ClearAndFree(builder);
        }

        public static string ReturnAndFree(StringBuilder builder)
        {
            SharedPools.Default<StringBuilder>().ForgetTrackedObject(builder);
            return builder.ToString();
        }
    }
}