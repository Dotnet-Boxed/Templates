namespace MvcBoilerplate.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MvcBoilerplate.Models;

    public class CarRepository : ICarRepository
    {
        private readonly List<Car> cars;

        public CarRepository()
        {
            this.cars = new List<Car>()
            {
                new Car()
                {
                    CarId = 1
                }
            };
        }

        public Task<Car> Add(Car car)
        {
            this.cars.Add(car);
            return Task.FromResult(car);
        }

        public Task Delete(Car car)
        {
            if (this.cars.Contains(car))
            {
                this.cars.Remove(car);
            }

            return Task.FromResult(0);
        }

        public Task<Car> Get(int carId)
        {
            var car = this.cars.FirstOrDefault(x => x.CarId == carId);
            return Task.FromResult(car);
        }

        public Task<Car> Update(Car car)
        {
            this.Delete(car);
            this.Add(car);
            return Task.FromResult(car);
        }
    }
}
