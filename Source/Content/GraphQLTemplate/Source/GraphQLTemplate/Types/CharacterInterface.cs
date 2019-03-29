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

            this.Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the character.");
            this.Field(x => x.Name, nullable: true)
                .Description("The name of the character.");
            this.Field(x => x.AppearsIn, type: typeof(ListGraphType<EpisodeEnumeration>))
                .Description("Which movie they appear in.");

            this.Field<ListGraphType<CharacterInterface>>(
                nameof(Character.Friends),
                "The friends of the character, or an empty list if they have none.");
        }
    }
}
