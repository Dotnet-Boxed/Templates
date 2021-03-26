namespace GraphQLTemplate.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.DataLoaders;
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

        public Task<IReadOnlyList<Droid>> GetDroidByIdsAsync(
            IDroidDataLoader droidDataLoader,
            List<Guid> ids,
            CancellationToken cancellationToken) =>
            droidDataLoader.LoadAsync(ids, cancellationToken);

        public Task<IQueryable<Human>> GetHumansAsync(CancellationToken cancellationToken) =>
            this.humanRepository.GetHumansAsync(cancellationToken);

        public Task<IReadOnlyList<Human>> GetHumansByIdsAsync(
            IHumanDataLoader humanDataLoader,
            List<Guid> ids,
            CancellationToken cancellationToken) =>
            humanDataLoader.LoadAsync(ids, cancellationToken);
    }
}
