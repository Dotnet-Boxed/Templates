namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public interface IDroidRepository
    {
        Task<Droid> GetDroid(string id, CancellationToken cancellationToken);

        List<Character> GetFriends(Droid droid, CancellationToken cancellationToken);
    }
}
