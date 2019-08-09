namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> If<T>(
            this IEnumerable<T> enumerable,
            bool condition,
            Func<IEnumerable<T>, IEnumerable<T>> action)
        {
            if (condition)
            {
                return action(enumerable);
            }

            return enumerable;
        }
    }
}
