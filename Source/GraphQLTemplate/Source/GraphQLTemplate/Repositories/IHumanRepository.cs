namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public interface IHumanRepository
    {
#if Subscriptions
        IObservable<Human> WhenHumanCreated { get; }

#endif
        Task<Human> AddHumanAsync(Human human, CancellationToken cancellationToken);

        Task<List<Character>> GetFriendsAsync(Human human, CancellationToken cancellationToken);

        Task<Human> GetHumanAsync(Guid id, CancellationToken cancellationToken);
    }
}
