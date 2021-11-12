namespace ApiTemplate.Mappers;

using ApiTemplate.Constants;
using ApiTemplate.ViewModels;
using Boxed.Mapping;

public class CarToCarMapper : IMapper<Models.Car, Car>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly LinkGenerator linkGenerator;

    public CarToCarMapper(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.linkGenerator = linkGenerator;
    }

    public void Map(Models.Car source, Car destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        destination.CarId = source.CarId;
        destination.Cylinders = source.Cylinders;
        destination.Make = source.Make;
        destination.Model = source.Model;
        destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
            this.httpContextAccessor.HttpContext!,
            CarsControllerRoute.GetCar,
            new { source.CarId })!);
    }
}
