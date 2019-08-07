namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public interface IDroidRepository
    {
        Task<Droid> GetDroid(Guid id, CancellationToken cancellationToken);

        Task<List<Droid>> GetDroids(
            int? first,
            DateTime? createdAfter,
            CancellationToken cancellationToken);

        Task<List<Droid>> GetDroidsReverse(
            int? last,
            DateTime? createdBefore,
            CancellationToken cancellationToken);

        Task<bool> GetHasNextPage(
            int? first,
            DateTime? createdAfter,
            CancellationToken cancellationToken);

        Task<bool> GetHasPreviousPage(
            int? last,
            DateTime? createdBefore,
            CancellationToken cancellationToken);

        Task<List<Character>> GetFriends(Droid droid, CancellationToken cancellationToken);

        Task<int> GetTotalCount(CancellationToken cancellationToken);
    }
}
