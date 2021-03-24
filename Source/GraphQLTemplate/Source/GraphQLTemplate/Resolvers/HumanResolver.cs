namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using HotChocolate.Resolvers;

    public class HumanResolver
    {
        public Task<Human> GetHumanAsync(IResolverContext context, Guid id) =>
            context.DataLoader<IHumanDataLoader>().LoadAsync(id, context.RequestAborted);
    }
}
