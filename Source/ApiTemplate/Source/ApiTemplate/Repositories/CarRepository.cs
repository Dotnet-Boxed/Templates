namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;
    using Ardalis.Specification;
    using Ardalis.Specification.EntityFrameworkCore;

    public class CarRepository : ICarRepository
    {
        private static readonly List<Car> Cars = new List<Car>()
        {
            new Car()
            {
                CarId = 1,
                Created = DateTimeOffset.UtcNow.AddDays(-8),
                Cylinders = 8,
                Make = "Lambourghini",
                Model = "Countach",
                Modified = DateTimeOffset.UtcNow.AddDays(-8),
            },
            new Car()
            {
                CarId = 2,
                Created = DateTimeOffset.UtcNow.AddDays(-5),
                Cylinders = 10,
                Make = "Mazda",
                Model = "Furai",
                Modified = DateTimeOffset.UtcNow.AddDays(-6),
            },
            new Car()
            {
                CarId = 3,
                Created = DateTimeOffset.UtcNow.AddDays(-10),
                Cylinders = 6,
                Make = "Honda",
                Model = "NSX",
                Modified = DateTimeOffset.UtcNow.AddDays(-3),
            },
            new Car()
            {
                CarId = 4,
                Created = DateTimeOffset.UtcNow.AddDays(-3),
                Cylinders = 6,
                Make = "Lotus",
                Model = "Esprit",
                Modified = DateTimeOffset.UtcNow.AddDays(-3),
            },
            new Car()
            {
                CarId = 5,
                Created = DateTimeOffset.UtcNow.AddDays(-12),
                Cylinders = 6,
                Make = "Mitsubishi",
                Model = "Evo",
                Modified = DateTimeOffset.UtcNow.AddDays(-2),
            },
            new Car()
            {
                CarId = 6,
                Created = DateTimeOffset.UtcNow.AddDays(-1),
                Cylinders = 12,
                Make = "McLaren",
                Model = "F1",
                Modified = DateTimeOffset.UtcNow.AddDays(-1),
            },
        };

        public Task<Car> AddAsync(Car car, CancellationToken cancellationToken)
        {
            if (car is null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            Cars.Add(car);
            car.CarId = Cars.Max(x => x.CarId) + 1;
            return Task.FromResult(car);
        }

        public Task DeleteAsync(Car car, CancellationToken cancellationToken)
        {
            if (Cars.Contains(car))
            {
                Cars.Remove(car);
            }

            return Task.CompletedTask;
        }

        public Task<Car> GetAsync(int carId, CancellationToken cancellationToken)
        {
            var car = Cars.FirstOrDefault(x => x.CarId == carId);
            return Task.FromResult(car);
        }

        public Task<List<Car>> GetCarsAsync(
            ISpecification<Car> specFirst,
            CancellationToken cancellationToken) =>
            Task.FromResult(ApplySpecification(specFirst).ToList());

        public Task<bool> GetHasAnyCarAsync(
            ISpecification<Car> specFirst,
            CancellationToken cancellationToken) =>
            Task.FromResult(ApplySpecification(specFirst).Any());

        private static IQueryable<Car> ApplySpecification(ISpecification<Car> spec)
        {
            var evaluator = new SpecificationEvaluator<Car>();
            return evaluator.GetQuery(Cars.AsQueryable(), spec);
        }

        public Task<int> GetTotalCountAsync(CancellationToken cancellationToken) => Task.FromResult(Cars.Count);

        public Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken)
        {
            if (car is null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            var existingCar = Cars.FirstOrDefault(x => x.CarId == car.CarId);
            existingCar.Cylinders = car.Cylinders;
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            return Task.FromResult(car);
        }
    }
}
