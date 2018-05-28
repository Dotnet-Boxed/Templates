namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public class DroidRepository : IDroidRepository
    {
        public Task<Droid> GetDroid(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(Database.Droids.FirstOrDefault(x => x.Id == id));

        public Task<List<Droid>> GetDroids(
            int? first,
            DateTime? createdAfter,
            CancellationToken cancellationToken) =>
            Task.FromResult(Database
                .Droids
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .If(first.HasValue, x => x.Take(first.Value))
                .ToList());

        public Task<List<Droid>> GetDroidsReverse(
            int? last,
            DateTime? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Database
                .Droids
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .If(last.HasValue, x => x.TakeLast(last.Value))
                .ToList());

        public Task<bool> GetHasNextPage(
            int? first,
            DateTime? createdAfter,
            CancellationToken cancellationToken) =>
            Task.FromResult(Database
                .Droids
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .Skip(first.Value)
                .Any());

        public Task<bool> GetHasPreviousPage(
            int? last,
            DateTime? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Database
                .Droids
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .SkipLast(last.Value)
                .Any());

        public Task<List<Character>> GetFriends(Droid droid, CancellationToken cancellationToken) =>
            Task.FromResult(Database.Characters.Where(x => droid.Friends.Contains(x.Id)).ToList());

        public Task<int> GetTotalCount(CancellationToken cancellationToken) =>
            Task.FromResult(Database.Droids.Count);
    }
}
