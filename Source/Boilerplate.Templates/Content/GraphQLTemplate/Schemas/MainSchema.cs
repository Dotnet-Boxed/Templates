namespace GraphQLTemplate.Schemas
{
    using GraphQL;
    using GraphQL.Types;
    using GraphQLTemplate.Mutations;
    using GraphQLTemplate.Queries;

    public class MainSchema : Schema
    {
#if (Mutations)
        public MainSchema(IDependencyResolver resolver, RootQuery query, RootMutation mutation)
#else
        public MainSchema(IDependencyResolver resolver, RootQuery query)
#endif
            : base(resolver)
        {
            this.Query = query;
#if (Mutations)
            this.Mutation = mutation;
#endif
        }
    }
}
