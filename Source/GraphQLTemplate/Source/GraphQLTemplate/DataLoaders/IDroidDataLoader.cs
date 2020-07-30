namespace GraphQLTemplate.DataLoaders
{
    using System;
    using GraphQLTemplate.Models;
    using GreenDonut;

    public interface IDroidDataLoader : IDataLoader<Guid, Droid>
    {
    }
}
