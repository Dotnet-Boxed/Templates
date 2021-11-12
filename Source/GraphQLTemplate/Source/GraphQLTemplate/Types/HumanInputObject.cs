namespace GraphQLTemplate.Types;

using GraphQLTemplate.Models;
using HotChocolate.Types;

public class HumanInputObject : InputObjectType<HumanInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<HumanInput> descriptor)
    {
        descriptor
            .Name("HumanInput")
            .Description("A humanoid creature from the Star Wars universe.");

        descriptor
            .Field(x => x.Name)
            .Description("The name of the human.");
        descriptor
            .Field(x => x.DateOfBirth)
            .Description("The humans date of birth.");
        descriptor
            .Field(x => x.HomePlanet)
            .Description("The home planet of the human.");
        descriptor
            .Field(x => x.AppearsIn)
            .Type<ListType<NonNullType<EpisodeEnumeration>>>()
            .Description("Which movie they appear in.");
    }
}
