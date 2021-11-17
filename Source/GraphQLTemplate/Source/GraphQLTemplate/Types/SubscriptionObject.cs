namespace GraphQLTemplate.Types;

using GraphQLTemplate.Resolvers;
using HotChocolate.Types;

/// <summary>
/// All subscriptions defined in the schema used to be notified of changes in data.
/// </summary>
/// <example>
/// <c>
/// subscription onHumanCreated {
///   onHumanCreated {
///     id
///     name
///     dateOfBirth
///     homePlanet
///     appearsIn
///   }
/// }
/// </c>
/// The is an example subscription to be notified when a human is created.
/// </example>
public class SubscriptionObject : ObjectType<SubscriptionResolver>
{
    protected override void Configure(IObjectTypeDescriptor<SubscriptionResolver> descriptor)
    {
        descriptor
            .Name("Subscription")
            .Description("The subscription type, represents all updates can be pushed to the client in real time over web sockets.");

        descriptor
            .Field(x => x.OnHumanCreatedAsync(default!, default!, default!))
            .Description("Subscribe to human created events.")
            .SubscribeToTopic<Guid>(nameof(SubscriptionResolver.OnHumanCreatedAsync));
    }
}
