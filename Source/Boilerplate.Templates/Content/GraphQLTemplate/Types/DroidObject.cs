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

            this.Field(x => x.Id, type: typeof(IdGraphType)).Description("The unique identifier of the droid.");
            this.Field(x => x.Name, nullable: true).Description("The name of the droid.");
            this.Field(x => x.PrimaryFunction, nullable: true).Description("The primary function of the droid.");

            this.FieldAsync<ListGraphType<CharacterInterface>, List<Character>>(
                "friends",
                resolve: context => droidRepository.GetFriends(context.Source, context.CancellationToken));
            this.Field<ListGraphType<EpisodeEnumeration>>("appearsIn", "Which movie they appear in.");

            this.Interface<CharacterInterface>();
        }
    }
}
