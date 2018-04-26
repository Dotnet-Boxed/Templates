namespace GraphQLTemplate.Types
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;

    public class CharacterInterface : InterfaceGraphType<Character>
    {
        public CharacterInterface()
        {
            this.Name = "Character";
            this.Description = "A character from the Star Wars universe";

            this.Field(x => x.Id, type: typeof(IdGraphType)).Description("The unique identifier of the character.");
            this.Field(x => x.Name, nullable: true).Description("The name of the character.");
            this.Field<ListGraphType<EpisodeEnumeration>>("appearsIn", "Which movie they appear in.");

            this.Field<ListGraphType<CharacterInterface>>(
                "friends",
                "The friends of the character, or an empty list if they have none.");
        }
    }
}
