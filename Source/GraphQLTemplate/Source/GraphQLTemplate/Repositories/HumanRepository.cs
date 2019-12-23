namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
#if Subscriptions
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
#endif
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public sealed class HumanRepository : IHumanRepository, IDisposable
    {
#if Subscriptions
        private readonly Subject<Human> whenHumanCreated;

        public HumanRepository() => this.whenHumanCreated = new Subject<Human>();

        public IObservable<Human> WhenHumanCreated => this.whenHumanCreated.AsObservable();

#endif
        public Task<Human> AddHumanAsync(Human human, CancellationToken cancellationToken)
        {
            if (human is null)
            {
                throw new ArgumentNullException(nameof(human));
            }

            human.Id = Guid.NewGuid();
            Database.Humans.Add(human);
#if Subscriptions
            this.whenHumanCreated.OnNext(human);
#endif
            return Task.FromResult(human);
        }

        public void Dispose() => this.whenHumanCreated.Dispose();

        public Task<List<Character>> GetFriendsAsync(Human human, CancellationToken cancellationToken)
        {
            if (human is null)
            {
                throw new ArgumentNullException(nameof(human));
            }

            return Task.FromResult(Database.Characters.Where(x => human.Friends.Contains(x.Id)).ToList());
        }

        public Task<Human> GetHumanAsync(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(Database.Humans.FirstOrDefault(x => x.Id == id));
    }
}
