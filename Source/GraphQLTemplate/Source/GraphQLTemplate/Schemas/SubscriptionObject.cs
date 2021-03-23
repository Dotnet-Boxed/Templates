namespace GraphQLTemplate.Schemas
{
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Resolvers;
    using HotChocolate.Types;

    /// <summary>
    /// All subscriptions defined in the schema used to be notified of changes in data.
    /// </summary>
    /// <example>
    /// <c>
    /// subscription whenHumanCreated {
    ///   humanCreated(homePlanets: ["Earth"])
    ///   {
    ///     id
    ///     name
    ///     dateOfBirth
    ///     homePlanet
    ///     appearsIn
    ///   }
    /// }
    /// </c>
    /// The is an example subscription to be notified when a human is created.
    /// </example>
    public class SubscriptionObject : ObjectType<SubscriptionResolvers>
    {
        private readonly IHumanRepository humanRepository;

        public SubscriptionObject(IHumanRepository humanRepository)
        {
            this.humanRepository = humanRepository;

            this.Name = "Subscription";
            this.Description = "The subscription type, represents all updates can be pushed to the client in real time over web sockets.";
        }

        protected override void Configure(IObjectTypeDescriptor<SubscriptionResolvers> descriptor)
        {
            // descriptor
            //     .Field("humanCreated")
            //     .Description("Subscribe to human created events.")
            //     .Argument("homePlanets", x => x.Description("The home planets of the humans created."))
            //     .Subscribe(async ctx =>
            //     {
            //         async foreach (var payload in await serviceBus.OnMessageReceiveAsync())
            //         {
            //             yield return payload;
            //         }
            //     });
            //
            // descriptor
            //     .Field(t => t.OnReview(default, default))
            //     .Type<NonNullType<ReviewType>>()
            //     .Argument("episode", arg => arg.Type<NonNullType<EpisodeType>>());

            // this.AddField(
            //     new EventStreamFieldType()
            //     {
            //         Name = "humanCreated",
            //         Description = "Subscribe to human created events.",
            //         Arguments = new QueryArguments(
            //             new QueryArgument<ListGraphType<StringGraphType>>()
            //             {
            //                 Name = "homePlanets",
            //             }),
            //         Type = typeof(HumanCreatedEvent),
            //         Resolver = new FuncFieldResolver<Human>(context => context.Source as Human),
            //         Subscriber = new EventStreamResolver<Human>(context =>
            //         {
            //             var homePlanets = context.GetArgument<List<string>>("homePlanets");
            //             return humanRepository
            //                 .WhenHumanCreated
            //                 .Where(x => homePlanets is null || homePlanets.Contains(x.HomePlanet));
            //         }),
            //     });
        }
    }
}
