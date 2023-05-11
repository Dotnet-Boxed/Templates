namespace Boxed.Templates.FunctionalTest;

using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static IDictionary<string, string> ToArguments(this string[] arguments) =>
        arguments
            .Select(static x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(static x => x.First(), static x => x.Last());

    public static IDictionary<string, string> ToArguments(this string[] arguments1, string[] arguments2) =>
        arguments1
            .Concat(arguments2)
            .Select(static x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(static x => x.First(), static x => x.Last());
}
