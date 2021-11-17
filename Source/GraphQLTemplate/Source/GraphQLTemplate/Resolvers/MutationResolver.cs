namespace GraphQLTemplate.Resolvers;

using Boxed.Mapping;
using GraphQLTemplate.Models;
using GraphQLTemplate.Repositories;
using HotChocolate;
#if Subscriptions
using HotChocolate.Subscriptions;
#endif

public class MutationResolver
{
    public async Task<Human> CreateHumanAsync(
        [Service] IImmutableMapper<HumanInput, Human> humanInputToHumanMapper,
        [Service] IHumanRepository humanRepository,
#if Subscriptions
        [Service] ITopicEventSender topicEventSender,
#endif
        HumanInput humanInput,
        CancellationToken cancellationToken)
    {
        var human = humanInputToHumanMapper.Map(humanInput);
        human = await humanRepository
            .AddHumanAsync(human, cancellationToken)
            .ConfigureAwait(false);
#if Subscriptions
        await topicEventSender
            .SendAsync(nameof(SubscriptionResolver.OnHumanCreatedAsync), human.Id, CancellationToken.None)
            .ConfigureAwait(false);
#endif
        return human;
    }
}
