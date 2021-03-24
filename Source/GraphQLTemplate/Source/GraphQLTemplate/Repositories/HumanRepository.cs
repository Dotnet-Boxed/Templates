namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
#if Subscriptions
#endif
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public sealed class HumanRepository : IHumanRepository// , IDisposable
    {
#if Subscriptions
        // private readonly Subject<Human> whenHumanCreated;

        // public HumanRepository() => this.whenHumanCreated = new Subject<Human>();

        public IObservable<Human> WhenHumanCreated => null!; // this.whenHumanCreated.AsObservable();

#endif
        public Task<Human> AddHumanAsync(Human human, CancellationToken cancellationToken)
        {
            Database.Humans.Add(human);
#if Subscriptions
            // this.whenHumanCreated.OnNext(newHuman);
#endif
            return Task.FromResult(human);
        }

        // public void Dispose() => this.whenHumanCreated.Dispose();

        public Task<List<Character>> GetFriendsAsync(Human human, CancellationToken
            cancellationToken) => Task.FromResult(Database.Characters.Where(x => human.Friends.Contains(x.Id)).ToList());

        public Task<IQueryable<Human>> GetHumansAsync(CancellationToken cancellationToken) =>
            Task.FromResult(Database.Humans.AsQueryable());

        public Task<IEnumerable<Human>> GetHumansAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) =>
            Task.FromResult(Database.Humans.Where(x => ids.Contains(x.Id)));
    }
}
