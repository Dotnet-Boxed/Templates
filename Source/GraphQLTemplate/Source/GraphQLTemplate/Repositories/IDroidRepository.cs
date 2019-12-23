namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public interface IDroidRepository
    {
        Task<Droid> GetDroidAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Droid>> GetDroidsAsync(
            int? first,
            DateTime? createdAfter,
            CancellationToken cancellationToken);

        Task<List<Droid>> GetDroidsReverseAsync(
            int? last,
            DateTime? createdBefore,
            CancellationToken cancellationToken);

        Task<bool> GetHasNextPageAsync(
            int? first,
            DateTime? createdAfter,
            CancellationToken cancellationToken);

        Task<bool> GetHasPreviousPageAsync(
            int? last,
            DateTime? createdBefore,
            CancellationToken cancellationToken);

        Task<List<Character>> GetFriendsAsync(Droid droid, CancellationToken cancellationToken);

        Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
    }
}
