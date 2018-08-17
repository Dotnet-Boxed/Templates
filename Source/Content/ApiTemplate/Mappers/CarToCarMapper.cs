namespace ApiTemplate.Mappers
{
    using System;
    using ApiTemplate.Constants;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class CarToCarMapper : IMapper<Models.Car, Car>
    {
        private readonly IUrlHelper urlHelper;

        public CarToCarMapper(IUrlHelper urlHelper) => this.urlHelper = urlHelper;

        public void Map(Models.Car source, Car destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.CarId = source.CarId;
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Url = this.urlHelper.AbsoluteRouteUrl(CarsControllerRoute.GetCar, new { source.CarId });
        }
    }
}
