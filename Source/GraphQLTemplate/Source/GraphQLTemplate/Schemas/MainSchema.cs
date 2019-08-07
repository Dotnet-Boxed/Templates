namespace GraphQLTemplate.Schemas
{
    using GraphQL;
    using GraphQL.Types;

    public class MainSchema : Schema
    {
        public MainSchema(
            QueryObject query,
#if Mutations
            MutationObject mutation,
#endif
#if Subscriptions
            SubscriptionObject subscription,
#endif
            IDependencyResolver resolver)

            : base(resolver)
        {
            this.Query = resolver.Resolve<QueryObject>();
#if Mutations
            this.Mutation = mutation;
#endif
#if Subscriptions
            this.Subscription = subscription;
#endif
        }
    }
}
