namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Linq;
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

        public Task<IQueryable<Droid>> GetDroidsAsync(IResolverContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.droidRepository.GetDroidsAsync(context.RequestAborted);
        }

        public Task<IQueryable<Human>> GetHumansAsync(IResolverContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.humanRepository.GetHumansAsync(context.RequestAborted);
        }
    }
}
