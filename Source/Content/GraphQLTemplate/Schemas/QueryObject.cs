namespace GraphQLTemplate.Schemas
{
    using System;
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Types;

    /// <summary>
    /// All queries defined in the schema used to retrieve data.
    /// </summary>
    /// <example>
    /// The is an example query to get a human and the details of their friends.
    /// <c>
    /// query getHuman {
    ///   human(id: "94fbd693-2027-4804-bf40-ed427fe76fda")
    ///   {
    ///     id,
    ///     name,
    ///     homePlanet,
    ///     appearsIn,
    ///     friends {
    ///       name
    ///       ... on Droid {
    ///         primaryFunction
    ///       }
    ///       ... on Human
    ///       {
    ///         homePlanet
    ///       }
    ///     }
    ///   }
    /// }
    /// </c>
    /// </example>
    public class QueryObject : ObjectGraphType<object>
    {
        public QueryObject(
            IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.FieldAsync<DroidObject, Droid>(
                "droid",
                "Get a droid by it's unique identifier.",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id",
                        Description = "The unique identifier of the droid.",
                    }),
                resolve: context =>
                    droidRepository.GetDroid(
                        context.GetArgument("id", defaultValue: new Guid("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68")),
                        context.CancellationToken));
            this.FieldAsync<HumanObject, Human>(
                "human",
                "Get a human by it's unique identifier.",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>()
                    {
                        Name = "id",
                        Description = "The unique identifier of the human.",
                    }),
                resolve: context => humanRepository.GetHuman(
                    context.GetArgument("id", defaultValue: new Guid("94fbd693-2027-4804-bf40-ed427fe76fda")),
                    context.CancellationToken));
        }
    }
}
