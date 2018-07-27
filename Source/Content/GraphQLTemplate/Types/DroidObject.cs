namespace GraphQLTemplate.Types
{
    using System.Collections.Generic;
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;

    public class DroidObject : ObjectGraphType<Droid>
    {
        public DroidObject(IDroidRepository droidRepository)
        {
            this.Name = "Droid";
            this.Description = "A mechanical creature in the Star Wars universe.";

            this.Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the droid.");
            this.Field(x => x.Name)
                .Description("The name of the droid.");
            this.Field(x => x.ChargePeriod)
                .Description("The time the droid can go without charging its batteries.");
            this.Field(x => x.Created)
                .Description("The date the droid was created.");
            this.Field(x => x.PrimaryFunction, nullable: true)
                .Description("The primary function of the droid.");
            this.Field(x => x.AppearsIn, type: typeof(ListGraphType<EpisodeEnumeration>))
                .Description("Which movie they appear in.");

            this.FieldAsync<ListGraphType<CharacterInterface>, List<Character>>(
                nameof(Droid.Friends),
                "The friends of the character, or an empty list if they have none.",
                resolve: context => droidRepository.GetFriends(context.Source, context.CancellationToken));

            this.Interface<CharacterInterface>();
        }
    }
}
