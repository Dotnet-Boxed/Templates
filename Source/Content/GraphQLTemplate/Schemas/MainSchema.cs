namespace GraphQLTemplate.Schemas
{
    using GraphQL;
    using GraphQL.Types;

    public class MainSchema : Schema
    {
        public MainSchema(
            RootQuery query,
#if (Mutations)
            RootMutation mutation,
#endif
#if (Subscriptions)
            RootSubscription subscription,
#endif
            IDependencyResolver resolver)

            : base(resolver)
        {
            this.Query = resolver.Resolve<RootQuery>();
#if (Mutations)
            this.Mutation = mutation;
#endif
#if (Subscriptions)
            this.Subscription = subscription;
#endif
        }
    }
}
