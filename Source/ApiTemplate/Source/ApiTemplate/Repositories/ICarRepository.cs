namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public interface ICarRepository
    {
        Task<Car> Add(Car car, CancellationToken cancellationToken);

        Task Delete(Car car, CancellationToken cancellationToken);

        Task<Car> Get(int carId, CancellationToken cancellationToken);

        Task<List<Car>> GetCars(
            int? first,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<List<Car>> GetCarsReverse(
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<bool> GetHasNextPage(
            int? first,
            DateTimeOffset? createdAfter,
            CancellationToken cancellationToken);

        Task<bool> GetHasPreviousPage(
            int? last,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<int> GetTotalCount(CancellationToken cancellationToken);

        Task<Car> Update(Car car, CancellationToken cancellationToken);
    }
}
