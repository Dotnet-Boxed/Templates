namespace ApiTemplate.Mappers
{
    using System;
    using ApiTemplate.Constants;
    using ApiTemplate.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.CarId = source.CarId;
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                CarsControllerRoute.GetCar,
                new { source.CarId }));
        }
    }
}
