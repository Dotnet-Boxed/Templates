namespace GraphQLTemplate.Resolvers
{
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class MutationResolvers
    {
        private readonly IImmutableMapper<HumanInput, Human> humanInputToHumanMapper;
        private readonly IHumanRepository humanRepository;

        public MutationResolvers(
            IImmutableMapper<HumanInput, Human> humanInputToHumanMapper,
            IHumanRepository humanRepository)
        {
            this.humanInputToHumanMapper = humanInputToHumanMapper;
            this.humanRepository = humanRepository;
        }

        public Task<Human> CreateHumanAsync(IResolverContext context, HumanInput humanInput)
        {
            var human = this.humanInputToHumanMapper.Map(humanInput);
            return this.humanRepository.AddHumanAsync(human, context.RequestAborted);
        }
    }
}
