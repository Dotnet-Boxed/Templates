namespace GraphQLTemplate.Resolvers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
#if Subscriptions
    using HotChocolate.Subscriptions;
#endif

    public class MutationResolver
    {
        private readonly IImmutableMapper<HumanInput, Human> humanInputToHumanMapper;
        private readonly IHumanRepository humanRepository;
#if Subscriptions
        private readonly ITopicEventSender topicEventSender;
#endif

        public MutationResolver(
            IImmutableMapper<HumanInput, Human> humanInputToHumanMapper,
#if Subscriptions
            IHumanRepository humanRepository,
            ITopicEventSender topicEventSender)
#else
            IHumanRepository humanRepository)
#endif
        {
            this.humanInputToHumanMapper = humanInputToHumanMapper;
            this.humanRepository = humanRepository;
#if Subscriptions
            this.topicEventSender = topicEventSender;
#endif
        }

        public async Task<Human> CreateHumanAsync(HumanInput humanInput, CancellationToken cancellationToken)
        {
            var human = this.humanInputToHumanMapper.Map(humanInput);
            human = await this.humanRepository
                .AddHumanAsync(human, cancellationToken)
                .ConfigureAwait(false);
#if Subscriptions
            await this.topicEventSender
                .SendAsync(nameof(SubscriptionResolver.OnHumanCreatedAsync), human.Id, CancellationToken.None)
                .ConfigureAwait(false);
#endif
            return human;
        }
    }
}
