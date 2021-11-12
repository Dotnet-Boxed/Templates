namespace ApiTemplate.Repositories;

using ApiTemplate.Models;

public class CarRepository : ICarRepository
{
    private static readonly List<Car> Cars = new()
    {
        new Car()
        {
            CarId = 1,
            Created = DateTimeOffset.UtcNow.AddDays(-8),
            Cylinders = 8,
            Make = "Lamborghini",
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
        ArgumentNullException.ThrowIfNull(car);

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

    public Task<Car?> GetAsync(int carId, CancellationToken cancellationToken)
    {
        var car = Cars.FirstOrDefault(x => x.CarId == carId);
        return Task.FromResult(car);
    }

    public Task<List<Car>> GetCarsAsync(
        int? first,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken) =>
        Task.FromResult(Cars
            .OrderBy(x => x.Created)
            .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter!.Value))
            .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore!.Value))
            .If(first.HasValue, x => x.Take(first!.Value))
            .ToList());

    public Task<List<Car>> GetCarsReverseAsync(
        int? last,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken) =>
        Task.FromResult(Cars
            .OrderBy(x => x.Created)
            .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter!.Value))
            .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore!.Value))
            .If(last.HasValue, x => x.TakeLast(last!.Value))
            .ToList());

    public Task<bool> GetHasNextPageAsync(
        int? first,
        DateTimeOffset? createdAfter,
        CancellationToken cancellationToken) =>
        Task.FromResult(Cars
            .OrderBy(x => x.Created)
            .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter!.Value))
            .If(first.HasValue, x => x.Skip(first!.Value))
            .Any());

    public Task<bool> GetHasPreviousPageAsync(
        int? last,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken) =>
        Task.FromResult(Cars
            .OrderBy(x => x.Created)
            .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore!.Value))
            .If(last.HasValue, x => x.SkipLast(last!.Value))
            .Any());

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken) => Task.FromResult(Cars.Count);

    public Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(car);

        var existingCar = Cars.First(x => x.CarId == car.CarId);
        existingCar.Cylinders = car.Cylinders;
        existingCar.Make = car.Make;
        existingCar.Model = car.Model;
        return Task.FromResult(car);
    }
}
