namespace GraphQLTemplate.Schemas
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Types;

    /// <summary>
    /// All mutations defined in the schema used to modify data.
    /// </summary>
    /// <example>
    /// This is an example mutation to create a new human.
    /// <c>
    /// mutation createHuman($human: HumanInput!) {
    ///   createHuman(human: $human)
    ///   {
    ///     id,
    ///     name
    ///   }
    /// }
    /// This is an example JSON of the variables you also need to specify to create a new human:
    /// {
    ///  "human": {
    ///     "name": "Muhammad Rehan Saeed",
    ///     "homePlanet": "Earth"
    ///   }
    /// }
    /// </c>
    /// </example>
    public class RootMutation : ObjectGraphType<object>
    {
        public RootMutation(IHumanRepository humanRepository)
        {
            this.Name = "Mutation";
            this.Description = "The mutation type, represents all updates we can make to our data.";

            this.FieldAsync<HumanObject, Human>(
                "createHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HumanInputObject>>()
                    {
                        Name = "human",
                        Description = "The human you want to create.",
                    }),
                resolve: context =>
                {
                    var human = context.GetArgument<Human>("human");
                    return humanRepository.AddHuman(human, context.CancellationToken);
                });
        }
    }
}
