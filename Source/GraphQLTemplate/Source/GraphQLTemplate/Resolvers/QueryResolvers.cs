namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class QueryResolvers
    {
        private readonly IDroidRepository droidRepository;
        private readonly IHumanRepository humanRepository;

        public QueryResolvers(
            IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            this.droidRepository = droidRepository;
            this.humanRepository = humanRepository;
        }

        public Task<Droid> GetDroidAsync(IResolverContext context, Guid id) =>
            this.droidRepository.GetDroidAsync(id, context.RequestAborted);

        public Task<Human> GetHumanAsync(IResolverContext context, Guid id) =>
            this.humanRepository.GetHumanAsync(id, context.RequestAborted);
    }
}
