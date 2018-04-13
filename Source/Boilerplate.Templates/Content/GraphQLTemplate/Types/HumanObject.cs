namespace GraphQLTemplate.Types
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;

    public class HumanObject : ObjectGraphType<Human>
    {
        public HumanObject(IHumanRepository humanRepository)
        {
            this.Name = "Human";
            this.Description = "A human being in the Star Wars universe.";

            this.Field(x => x.Id).Description("The unique identifier of the human.");
            this.Field(x => x.Name, nullable: true).Description("The name of the human.");
            this.Field(x => x.HomePlanet, nullable: true).Description("The home planet of the human.");

            this.Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => humanRepository.GetFriends(context.Source, context.CancellationToken));
            this.Field<ListGraphType<EpisodeEnumeration>>("appearsIn", "Which movie they appear in.");

            this.Interface<CharacterInterface>();
        }
    }
}
