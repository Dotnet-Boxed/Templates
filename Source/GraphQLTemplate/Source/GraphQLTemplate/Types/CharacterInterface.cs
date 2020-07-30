namespace GraphQLTemplate.Types
{
    using System;
    using GraphQLTemplate.Models;
    using HotChocolate.Types;

    public class CharacterInterface : InterfaceType<Character>
    {
        protected override void Configure(IInterfaceTypeDescriptor<Character> descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor.Name("Character");
            descriptor.Description("A character from the Star Wars universe");

            descriptor
                .Field(x => x.Id)
                .Type<NonNullType<IdType>>()
                .Description("The unique identifier of the character.");
            descriptor
                .Field(x => x.Name)
                .Type<StringType>()
                .Description("The name of the character.");
            descriptor
                .Field(x => x.AppearsIn)
                .Type<ListType<NonNullType<EpisodeEnumeration>>>()
                .Description("Which movie they appear in.");
            descriptor
                .Field(x => x.Friends)
                .Type<ListType<NonNullType<CharacterInterface>>>()
                .Description("The friends of the character, or an empty list if they have none.");
        }
    }
}
