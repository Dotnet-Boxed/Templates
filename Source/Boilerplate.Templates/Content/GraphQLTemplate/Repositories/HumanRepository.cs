namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;

    public class HumanRepository : IHumanRepository
    {
        public Task<Human> AddHuman(Human human, CancellationToken cancellationToken)
        {
            human.Id = Guid.NewGuid().ToString();
            Database.Humans.Add(human);
            return Task.FromResult(human);
        }

        public List<Character> GetFriends(Human human, CancellationToken cancellationToken) =>
            Database.Characters.Where(x => human.Friends.Contains(x.Id)).ToList();

        public Task<Human> GetHuman(string id, CancellationToken cancellationToken) =>
            Task.FromResult(Database.Humans.FirstOrDefault(x => x.Id == id));
    }
}
