namespace GraphQLTemplate.Repositories;

public static class EnumerableExtensions
{
    public static IEnumerable<T> If<T>(this IEnumerable<T> enumerable, bool condition, Func<IEnumerable<T>, IEnumerable<T>> action)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        ArgumentNullException.ThrowIfNull(action);

        if (condition)
        {
            return action(enumerable);
        }

        return enumerable;
    }
}
