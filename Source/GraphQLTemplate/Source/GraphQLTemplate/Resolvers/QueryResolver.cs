namespace GraphQLTemplate.Resolvers
{
    using System.Linq;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using HotChocolate.Resolvers;

    public class QueryResolver
    {
        private readonly IDroidRepository droidRepository;
        private readonly IHumanRepository humanRepository;

        public QueryResolver(
            IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            this.droidRepository = droidRepository;
            this.humanRepository = humanRepository;
        }

        public Task<IQueryable<Droid>> GetDroidsAsync(IResolverContext context) =>
            this.droidRepository.GetDroidsAsync(context.RequestAborted);

        public Task<IQueryable<Human>> GetHumansAsync(IResolverContext context) =>
            this.humanRepository.GetHumansAsync(context.RequestAborted);
    }
}
