namespace GraphQLTemplate.DataLoaders;

using GraphQLTemplate.Models;
using GraphQLTemplate.Repositories;
using GreenDonut;

public class DroidDataLoader : BatchDataLoader<Guid, Droid>, IDroidDataLoader
{
    private readonly IDroidRepository repository;

    public DroidDataLoader(IDroidRepository repository, IBatchScheduler batchScheduler, DataLoaderOptions options)
        : base(batchScheduler, options) =>
        this.repository = repository;

    protected override async Task<IReadOnlyDictionary<Guid, Droid>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var droids = await this.repository.GetDroidsAsync(keys, cancellationToken).ConfigureAwait(false);
        return droids.ToDictionary(x => x.Id);
    }
}
