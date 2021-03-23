namespace GraphQLTemplate.Resolvers
{
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class MutationResolvers
    {
        private readonly IHumanRepository humanRepository;

        public MutationResolvers(IHumanRepository humanRepository) => this.humanRepository = humanRepository;

        public Task<Human> CreateHumanAsync(IResolverContext context, HumanInput human) =>
            this.humanRepository.AddHumanAsync(human, context.RequestAborted);
    }
}
