namespace GraphQLTemplate.Schemas
{
    using System;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class Query
    {
        private readonly IDroidRepository droidRepository;
        private readonly IHumanRepository humanRepository;

        public Query(
            IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            this.droidRepository = droidRepository;
            this.humanRepository = humanRepository;
        }

        public Task<Droid> Droid(IResolverContext context, Guid id) =>
            this.droidRepository.GetDroidAsync(id, context.RequestAborted);
    }
}
