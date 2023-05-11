namespace GraphQLTemplate.Types;

using GraphQLTemplate.Models;
using GraphQLTemplate.Resolvers;
using HotChocolate.Types;

public class DroidObject : ObjectType<Droid>
{
    protected override void Configure(IObjectTypeDescriptor<Droid> descriptor)
    {
        descriptor
            .Name("Droid")
            .Description("A mechanical creature in the Star Wars universe.")
            .Implements<CharacterInterface>();

        descriptor
            .ImplementsNode()
            .IdField(static x => x.Id)
            .ResolveNodeWith<DroidResolver>(static x => x.GetDroidAsync(default!, default!, default!));
        descriptor
            .Field(static x => x.Name)
            .Description("The name of the droid.");
        descriptor
            .Field(static x => x.ChargePeriod)
            .Description("The time the droid can go without charging its batteries.");
        descriptor
           .Field(static x => x.Manufactured)
           .Description("The date the droid was manufactured.");
        descriptor
            .Field(static x => x.PrimaryFunction)
            .Description("The primary function of the droid.");
        descriptor
            .Field(static x => x.AppearsIn)
            .Description("Which movie they appear in.");
        descriptor
            .Field(static x => x.Friends)
            .Type<NonNullType<ListType<NonNullType<CharacterInterface>>>>()
            .Description("The friends of the character, or an empty list if they have none.")
            .ResolveWith<DroidResolver>(static x => x.GetFriendsAsync(default!, default!, default!));
    }
}
