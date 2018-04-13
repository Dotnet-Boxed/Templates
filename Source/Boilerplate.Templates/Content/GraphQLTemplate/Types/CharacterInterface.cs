namespace GraphQLTemplate.Types
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;

    public class CharacterInterface : InterfaceGraphType<Character>
    {
        public CharacterInterface()
        {
            this.Name = "Character";

            this.Field(x => x.Id).Description("The unique identifier of the character.");
            this.Field(x => x.Name, nullable: true).Description("The name of the character.");

            this.Field<ListGraphType<CharacterInterface>>("friends");
            this.Field<ListGraphType<EpisodeEnumeration>>("appearsIn", "Which movie they appear in.");
        }
    }
}
