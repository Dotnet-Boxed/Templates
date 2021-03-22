namespace GraphQLTemplate.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GreenDonut;

    public class HumanDataLoader : DataLoaderBase<Guid, Human>, IHumanDataLoader
    {
        private readonly IHumanRepository repository;

        public HumanDataLoader(IBatchScheduler batchScheduler, IHumanRepository repository)
            : base(batchScheduler, new DataLoaderOptions<Guid>()) =>
            this.repository = repository;

        protected override async ValueTask<IReadOnlyList<Result<Human>>> FetchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            var droids = await this.repository.GetHumansAsync(keys, cancellationToken).ConfigureAwait(false);
            return droids.Select(x => Result<Human>.Resolve(x)).ToList();
        }
    }
}
