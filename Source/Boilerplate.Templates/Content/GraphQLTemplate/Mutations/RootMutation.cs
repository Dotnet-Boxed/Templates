namespace GraphQLTemplate.Mutations
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Types;

    /// <example>
    /// This is an example JSON request for a mutation
    /// {
    ///   "query": "mutation ($human:HumanInput!){ createHuman(human: $human) { id name } }",
    ///   "variables": {
    ///     "human": {
    ///       "name": "Boba Fett"
    ///     }
    ///   }
    /// }
    /// </example>
    public class RootMutation : ObjectGraphType<object>
    {
        public RootMutation(IHumanRepository humanRepository)
        {
            this.Name = "Mutation";
            this.Description = "The mutation type, represents all updates we can make to our data.";

            this.Field<HumanObject>(
                "createHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HumanInputObject>>()
                    {
                        Name = "human",
                        Description = "A human being in the Star Wars universe."
                    }),
                resolve: context =>
                {
                    var human = context.GetArgument<Human>("human");
                    return humanRepository.AddHuman(human, context.CancellationToken);
                });
        }
    }
}
