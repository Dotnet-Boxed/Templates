namespace GraphQLTemplate.Queries
{
    using System;
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Types;

    public class RootQuery : ObjectGraphType<object>
    {
        public RootQuery(
            IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.FieldAsync<DroidObject, Droid>(
                "droid",
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
