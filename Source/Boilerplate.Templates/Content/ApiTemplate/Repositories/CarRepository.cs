namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public class CarRepository : ICarRepository
    {
        private static readonly List<Car> cars;

        static CarRepository()
        {
            cars = new List<Car>()
            {
                new Car()
                {
                    CarId = 1,
                    Created = DateTimeOffset.UtcNow.AddDays(-8),
                    Cylinders = 8,
                    Make = "Lambourghini",
                    Model = "Countach",
                    Modified = DateTimeOffset.UtcNow.AddDays(-8)
                },
                new Car()
                {
                    CarId = 2,
                    Created = DateTimeOffset.UtcNow.AddDays(-7),
                    Cylinders = 10,
                    Make = "Mazda",
                    Model = "Furai",
                    Modified = DateTimeOffset.UtcNow.AddDays(-6)
                },
                new Car()
                {
                    CarId = 3,
                    Created = DateTimeOffset.UtcNow.AddDays(-7),
                    Cylinders = 6,
                    Make = "Honda",
                    Model = "NSX",
                    Modified = DateTimeOffset.UtcNow.AddDays(-3),
                },
                new Car()
                {
                    CarId = 4,
                    Created = DateTimeOffset.UtcNow.AddDays(-5),
                    Cylinders = 6,
                    Make = "Lotus",
                    Model = "Esprit",
                    Modified = DateTimeOffset.UtcNow.AddDays(-3),
                },
                new Car()
                {
                    CarId = 5,
                    Created = DateTimeOffset.UtcNow.AddDays(-4),
                    Cylinders = 6,
                    Make = "Mitsubishi",
                    Model = "Evo",
                    Modified = DateTimeOffset.UtcNow.AddDays(-2),
                },
                new Car()
                {
                    CarId = 6,
                    Created = DateTimeOffset.UtcNow.AddDays(-4),
                    Cylinders = 12,
                    Make = "McLaren",
                    Model = "F1",
                    Modified = DateTimeOffset.UtcNow.AddDays(-1),
                }
            };
        }

        public Task<Car> Add(Car car)
        {
            cars.Add(car);
            car.CarId = cars.Max(x => x.CarId) + 1;
            return Task.FromResult(car);
        }

        public Task Delete(Car car)
        {
            if (cars.Contains(car))
            {
                cars.Remove(car);
            }

            return Task.FromResult(0);
        }

        public Task<Car> Get(int carId)
        {
            var car = cars.FirstOrDefault(x => x.CarId == carId);
            return Task.FromResult(car);
        }

        public Task<ICollection<Car>> GetPage(int page, int count)
        {
            var pageCars = cars
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageCars.Count == 0)
            {
                pageCars = null;
            }

            return Task.FromResult((ICollection<Car>)pageCars);
        }

        public Task<int> GetTotalPages(int count)
        {
            var totalPages = (int)Math.Ceiling(cars.Count / (double)count);
            return Task.FromResult(totalPages);
        }

        public Task<Car> Update(Car car)
        {
            var existingCar = cars.FirstOrDefault(x => x.CarId == car.CarId);
            existingCar.Cylinders = car.Cylinders;
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            return Task.FromResult(car);
        }
    }
}
