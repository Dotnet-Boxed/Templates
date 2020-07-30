namespace GraphQLTemplate.Types
{
    using System;
    using GraphQLTemplate.Models;
    using HotChocolate.Types;

    public class HumanInputObject : InputObjectType<Human>
    {
        protected override void Configure(IInputObjectTypeDescriptor<Human> descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor
                .Name("HumanInput")
                .Description("A humanoid creature from the Star Wars universe.");

            descriptor
                .Field(x => x.Name)
                .Type<StringType>()
                .Description("The name of the human.");
            descriptor
                .Field(x => x.DateOfBirth)
                .Type<NonNullType<DateType>>()
                .Description("The humans date of birth.");
            descriptor
                .Field(x => x.HomePlanet)
                .Type<StringType>()
                .Description("The home planet of the human.");
            descriptor
                .Field(x => x.AppearsIn)
                .Type<ListType<NonNullType<EpisodeEnumeration>>>()
                .Description("Which movie they appear in.");
        }
    }
}
