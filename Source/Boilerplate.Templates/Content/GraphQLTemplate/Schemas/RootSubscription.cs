namespace GraphQLTemplate.Schemas
{
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using GraphQL.Resolvers;
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Types;

    public class RootSubscription : ObjectGraphType<object>
    {
        public RootSubscription(IHumanRepository humanRepository)
        {
            this.Name = "Subscription";
            this.Description = "TODO";

            this.AddField(
                new EventStreamFieldType()
                {
                    Name = "humanCreated",
                    Arguments = new QueryArguments(
                        new QueryArgument<ListGraphType<StringGraphType>>()
                        {
                            Name = "homePlanets"
                        }),
                    Type = typeof(HumanCreatedEvent),
                    Resolver = new FuncFieldResolver<Human>(context => context.Source as Human),
                    Subscriber = new EventStreamResolver<Human>(context =>
                    {
                        var homePlanets = context.GetArgument<List<string>>("homePlanets");
                        return humanRepository
                            .WhenHumanCreated
                            .Where(x => homePlanets == null || homePlanets.Contains(x.HomePlanet));
                    }),
                });
        }
    }
}
