namespace GraphQLTemplate.Resolvers;

using GraphQLTemplate.DataLoaders;
using GraphQLTemplate.Models;
using GraphQLTemplate.Repositories;
using HotChocolate;

public class HumanResolver
{
    public Task<Human> GetHumanAsync(IHumanDataLoader humanDataLoader, Guid id, CancellationToken cancellationToken) =>
        humanDataLoader.LoadAsync(id, cancellationToken);

    public Task<List<Character>> GetFriendsAsync(
        [Service] IHumanRepository humanRepository,
        [Parent] Human human,
        CancellationToken cancellationToken) =>
        humanRepository.GetFriendsAsync(human, cancellationToken);
}
