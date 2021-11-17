namespace GraphQLTemplate.DataLoaders;

using GraphQLTemplate.Models;
using GraphQLTemplate.Repositories;
using GreenDonut;

public class HumanDataLoader : BatchDataLoader<Guid, Human>, IHumanDataLoader
{
    private readonly IHumanRepository repository;

    public HumanDataLoader(IHumanRepository repository, IBatchScheduler batchScheduler, DataLoaderOptions? options)
        : base(batchScheduler, options) =>
        this.repository = repository;

    protected override async Task<IReadOnlyDictionary<Guid, Human>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var droids = await this.repository.GetHumansAsync(keys, cancellationToken).ConfigureAwait(false);
        return droids.ToDictionary(x => x.Id);
    }
}
