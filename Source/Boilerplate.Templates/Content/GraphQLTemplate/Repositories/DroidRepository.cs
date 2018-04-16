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

        public List<Character> GetFriends(Droid droid, CancellationToken cancellationToken) =>
            Database.Characters.Where(x => droid.Friends.Contains(x.Id)).ToList();
    }
}
