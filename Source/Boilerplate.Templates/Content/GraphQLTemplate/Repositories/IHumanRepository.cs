namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public interface IHumanRepository
    {
        Task<Human> AddHuman(Human human, CancellationToken cancellationToken);

        List<Character> GetFriends(Human human, CancellationToken cancellationToken);

        Task<Human> GetHuman(Guid id, CancellationToken cancellationToken);
    }
}
