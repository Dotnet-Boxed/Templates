namespace GraphQLTemplate.Resolvers
{
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;
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

        public async Task<Human> CreateHumanAsync(IResolverContext context, HumanInput humanInput)
        {
            var human = this.humanInputToHumanMapper.Map(humanInput);
            human = await this.humanRepository
                .AddHumanAsync(human, context.RequestAborted)
                .ConfigureAwait(false);
#if Subscriptions
            await this.topicEventSender
                .SendAsync(nameof(SubscriptionResolver.OnHumanCreatedAsync), human.Id)
                .ConfigureAwait(false);
#endif
            return human;
        }
    }
}
