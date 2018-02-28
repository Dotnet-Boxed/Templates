namespace ApiTemplate.Translators
{
    using System;
    using ApiTemplate.ViewModels;
    using Boilerplate.Mapping;

    public class CarToCarMapper : IMapper<Models.Car, Car>
    {
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
        }
    }
}
