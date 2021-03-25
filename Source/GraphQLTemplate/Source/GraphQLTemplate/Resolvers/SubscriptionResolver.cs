namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using HotChocolate;
    using HotChocolate.Resolvers;

    public class SubscriptionResolver
    {
        public Task<Human> OnHumanCreatedAsync(IResolverContext context, [EventMessage] Guid id) =>
            context.DataLoader<IHumanDataLoader>().LoadAsync(id, context.RequestAborted);
    }
}
