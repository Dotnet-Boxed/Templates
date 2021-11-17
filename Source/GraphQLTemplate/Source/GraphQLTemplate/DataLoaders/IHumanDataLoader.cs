namespace GraphQLTemplate.DataLoaders;

using GraphQLTemplate.Models;
using GreenDonut;

public interface IHumanDataLoader : IDataLoader<Guid, Human>
{
}
