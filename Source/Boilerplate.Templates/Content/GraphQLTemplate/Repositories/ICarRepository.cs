namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public interface ICarRepository
    {
        Task<Car> Add(Car car, CancellationToken cancellationToken);

        Task Delete(Car car, CancellationToken cancellationToken);

        Task<Car> Get(int carId, CancellationToken cancellationToken);

        Task<ICollection<Car>> GetPage(int page, int count, CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken);

        Task<Car> Update(Car car, CancellationToken cancellationToken);
    }
}
