namespace GraphQLTemplate.Repositories;

using GraphQLTemplate.Models;

public sealed class HumanRepository : IHumanRepository
{
    public Task<Human> AddHumanAsync(Human human, CancellationToken cancellationToken)
    {
        Database.Humans.Add(human);
        return Task.FromResult(human);
    }

    public Task<List<Character>> GetFriendsAsync(Human human, CancellationToken
        cancellationToken) => Task.FromResult(Database.Characters.Where(x => human.Friends.Contains(x.Id)).ToList());

    public Task<IQueryable<Human>> GetHumansAsync(CancellationToken cancellationToken) =>
        Task.FromResult(Database.Humans.AsQueryable());

    public Task<IEnumerable<Human>> GetHumansAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) =>
        Task.FromResult(Database.Humans.Where(x => ids.Contains(x.Id)));
}
