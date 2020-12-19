namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public interface ICarRepository
    {
        Task<Car> AddAsync(Car car, CancellationToken cancellationToken);

        Task DeleteAsync(Car car, CancellationToken cancellationToken);

        Task<Car?> GetAsync(int carId, CancellationToken cancellationToken);

        Task<List<Car>> GetCarsAsync(
            int? first,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<List<Car>> GetCarsReverseAsync(
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<bool> GetHasNextPageAsync(
            int? first,
            DateTimeOffset? createdAfter,
            CancellationToken cancellationToken);

        Task<bool> GetHasPreviousPageAsync(
            int? last,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<int> GetTotalCountAsync(CancellationToken cancellationToken);

        Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken);
    }
}
