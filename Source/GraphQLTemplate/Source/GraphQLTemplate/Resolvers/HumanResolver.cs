namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class HumanResolver
    {
        private readonly IHumanRepository humanRepository;

        public HumanResolver(IHumanRepository humanRepository) => this.humanRepository = humanRepository;

        public Task<Human> GetHumanAsync(IResolverContext context, Guid id) =>
            context.DataLoader<IHumanDataLoader>().LoadAsync(id, context.RequestAborted);

        public Task<List<Character>> GetFriendsAsync(IResolverContext context) =>
            this.humanRepository.GetFriendsAsync(context.Parent<Human>(), context.RequestAborted);
    }
}
