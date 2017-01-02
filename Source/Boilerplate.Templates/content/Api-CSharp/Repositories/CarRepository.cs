namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public class CarRepository : ICarRepository
    {
        private readonly List<Car> cars;

        public CarRepository()
        {
            this.cars = new List<Car>()
            {
                new Car()
                {
                    CarId = 1,
                    Cylinders = 8,
                    Make = "Lambourghini",
                    Model = "Countach"
                },
                new Car()
                {
                    CarId = 2,
                    Cylinders = 10,
                    Make = "Mazda",
                    Model = "Furai"
                },
                new Car()
                {
                    CarId = 3,
                    Cylinders = 6,
                    Make = "Honda",
                    Model = "NSX"
                },
                new Car()
                {
                    CarId = 4,
                    Cylinders = 6,
                    Make = "Lotus",
                    Model = "Esprit"
                },
                new Car()
                {
                    CarId = 5,
                    Cylinders = 6,
                    Make = "Mitsubishi",
                    Model = "Evo"
                },
                new Car()
                {
                    CarId = 6,
                    Cylinders = 12,
                    Make = "McLaren",
                    Model = "F1"
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

        public Task<ICollection<Car>> GetPage(int page, int count)
        {
            var cars = this.cars
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (cars.Count == 0)
            {
                cars = null;
            }

            return Task.FromResult((ICollection<Car>)cars);
        }

        public Task<int> GetTotalPages(int page, int count)
        {
            return Task.FromResult(this.cars.Count == 0 ? 1 : ((this.cars.Count - 1) / count) + 1);
        }

        public Task<Car> Update(Car car)
        {
            this.Delete(car);
            this.Add(car);
            return Task.FromResult(car);
        }
    }
}
