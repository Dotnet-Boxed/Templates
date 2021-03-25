namespace GraphQLTemplate.Resolvers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;

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

        public Task<IQueryable<Droid>> GetDroidsAsync(CancellationToken cancellationToken) =>
            this.droidRepository.GetDroidsAsync(cancellationToken);

        public Task<IQueryable<Human>> GetHumansAsync(CancellationToken cancellationToken) =>
            this.humanRepository.GetHumansAsync(cancellationToken);
    }
}
