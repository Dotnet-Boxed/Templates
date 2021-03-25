namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using HotChocolate;

    public class SubscriptionResolver
    {
        public Task<Human> OnHumanCreatedAsync(
            IHumanDataLoader humanDataLoader,
            [EventMessage] Guid id,
            CancellationToken cancellationToken) =>
            humanDataLoader.LoadAsync(id, cancellationToken);
    }
}
