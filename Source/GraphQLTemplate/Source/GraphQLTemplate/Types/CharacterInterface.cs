namespace GraphQLTemplate.Types;

using GraphQLTemplate.Models;
using HotChocolate.Types;

public class CharacterInterface : InterfaceType<Character>
{
    protected override void Configure(IInterfaceTypeDescriptor<Character> descriptor)
    {
        descriptor
            .Name("Character")
            .Description("A character from the Star Wars universe");

        descriptor
            .Field(x => x.Id)
            .Type<NonNullType<IdType>>()
            .Description("The unique identifier of the character.");
        descriptor
            .Field(x => x.Name)
            .Description("The name of the character.");
        descriptor
            .Field(x => x.AppearsIn)
            .Description("Which movie they appear in.");
        descriptor
            .Field(x => x.Friends)
            .Type<NonNullType<ListType<NonNullType<CharacterInterface>>>>()
            .Description("The friends of the character, or an empty list if they have none.");
    }
}
