namespace GraphQLTemplate.Schemas
{
    using GraphQL;
    using GraphQL.Types;
    using GraphQLTemplate.Mutations;
    using GraphQLTemplate.Queries;

    public class MainSchema : Schema
    {
        public MainSchema(IDependencyResolver resolver, RootQuery query, RootMutation mutation)
            : base(resolver)
        {
            this.Query = query;
            this.Mutation = mutation;
        }
    }
}
