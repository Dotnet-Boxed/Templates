namespace MvcBoilerplate.Repositories
{
    using System.Threading.Tasks;
    using MvcBoilerplate.Models;

    public interface ICarRepository
    {
        Task<Car> Add(Car car);

        Task Delete(Car car);

        Task<Car> Get(int carId);

        Task<Car> Update(Car car);
    }
}
