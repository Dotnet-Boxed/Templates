namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using HotChocolate.Resolvers;

    public class DroidResolver
    {
        public Task<Droid> GetDroidAsync(IResolverContext context, Guid id) =>
            context.DataLoader<IDroidDataLoader>().LoadAsync(id, context.RequestAborted);
    }
}
