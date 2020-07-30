namespace GraphQLTemplate.DataLoaders
{
    using System;
    using GraphQLTemplate.Models;
    using GreenDonut;

    public interface IHumanDataLoader : IDataLoader<Guid, Human>
    {
    }
}
