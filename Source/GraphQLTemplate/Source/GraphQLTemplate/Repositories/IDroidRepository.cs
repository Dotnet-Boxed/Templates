namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public interface IDroidRepository
    {
        Task<IQueryable<Droid>> GetDroidsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Droid>> GetDroidsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        Task<List<Character>> GetFriendsAsync(Droid droid, CancellationToken cancellationToken);
    }
}
