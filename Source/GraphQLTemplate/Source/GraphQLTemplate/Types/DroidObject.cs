namespace GraphQLTemplate.Types
{
    using System;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;
    using HotChocolate.Types;
    using HotChocolate.Types.Relay;

    public class DroidObject : ObjectType<Droid>
    {
        private readonly IDroidRepository droidRepository;

        public DroidObject(IDroidRepository droidRepository) => this.droidRepository = droidRepository;

        protected override void Configure(IObjectTypeDescriptor<Droid> descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor
                .Name("Droid")
                .Description("A mechanical creature in the Star Wars universe.")
                .Implements<CharacterInterface>();

            descriptor
                .AsNode()
                .IdField(x => x.Id)
                .NodeResolver((context, id) => context.DataLoader<IDroidDataLoader>().LoadAsync(id, context.RequestAborted));
            descriptor
                .Field(x => x.Name)
                .Type<StringType>()
                .Description("The name of the droid.");
            descriptor
                .Field(x => x.ChargePeriod)
                .Type<NonNullType<TimeSpanType>>()
                .Description("The time the droid can go without charging its batteries.");
            descriptor
               .Field(x => x.Manufactured)
               .Type<NonNullType<DateTimeType>>()
               .Description("The date the droid was manufactured.");
            descriptor
                .Field(x => x.PrimaryFunction)
                .Type<StringType>()
                .Description("The primary function of the droid.");
            descriptor
                .Field(x => x.AppearsIn)
                .Type<ListType<NonNullType<EpisodeEnumeration>>>()
                .Description("Which movie they appear in.");
            descriptor
                .Field(x => x.Friends)
                .Type<ListType<NonNullType<CharacterInterface>>>()
                .Description("The friends of the character, or an empty list if they have none.")
                .Resolver(context => this.droidRepository.GetFriendsAsync(context.Parent<Droid>(), context.RequestAborted));
        }
    }
}
