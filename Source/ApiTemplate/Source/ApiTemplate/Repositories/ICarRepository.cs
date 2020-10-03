namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;
    using Ardalis.Specification;

    public interface ICarRepository
    {
        Task<Car> AddAsync(Car car, CancellationToken cancellationToken);

        Task DeleteAsync(Car car, CancellationToken cancellationToken);

        Task<Car> GetAsync(int carId, CancellationToken cancellationToken);

        Task<List<Car>> GetCarsAsync(
            ISpecification<Car> spec,
            CancellationToken cancellationToken);

        Task<bool> GetHasAnyCarAsync(
            ISpecification<Car> spec,
            CancellationToken cancellationToken);

        Task<int> GetTotalCountAsync(CancellationToken cancellationToken);

        Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken);
    }
}
