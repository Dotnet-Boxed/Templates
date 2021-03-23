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
    using GraphQLTemplate.Services;

    public sealed class HumanRepository : IHumanRepository// , IDisposable
    {
        private readonly IClockService clockService;

        public HumanRepository(IClockService clockService) => this.clockService = clockService;

#if Subscriptions
        // private readonly Subject<Human> whenHumanCreated;

        // public HumanRepository() => this.whenHumanCreated = new Subject<Human>();

        public IObservable<Human> WhenHumanCreated => null!; // this.whenHumanCreated.AsObservable();

#endif
        public Task<Human> AddHumanAsync(HumanInput humanInput, CancellationToken cancellationToken)
        {
            var now = this.clockService.UtcNow;
            var human = new Human()
            {
                Id = Guid.NewGuid(),
                Name = humanInput.Name,
                AppearsIn = humanInput.AppearsIn,
                DateOfBirth = humanInput.DateOfBirth,
                HomePlanet = humanInput.HomePlanet,
                Created = now,
                Modified = now,
            };
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
