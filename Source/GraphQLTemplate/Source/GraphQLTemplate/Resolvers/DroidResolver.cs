namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class DroidResolver
    {
        private readonly IDroidRepository droidRepository;

        public DroidResolver(IDroidRepository droidRepository) => this.droidRepository = droidRepository;

        public Task<Droid> GetDroidAsync(IResolverContext context, Guid id) =>
            context.DataLoader<IDroidDataLoader>().LoadAsync(id, context.RequestAborted);

        public Task<List<Character>> GetFriendsAsync(IResolverContext context) =>
            this.droidRepository.GetFriendsAsync(context.Parent<Droid>(), context.RequestAborted);
    }
}
