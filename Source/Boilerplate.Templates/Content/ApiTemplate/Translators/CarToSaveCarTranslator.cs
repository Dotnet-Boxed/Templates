namespace ApiTemplate.Translators
{
    using System;
    using Boilerplate;
    using ApiTemplate.Services;
    using ApiTemplate.ViewModels;

    public class CarToSaveCarTranslator :  ITranslator<Models.Car, SaveCar>, ITranslator<SaveCar, Models.Car>
    {
        private readonly IClockService clockService;

        public CarToSaveCarTranslator(IClockService clockService) =>
            this.clockService = clockService;

        public void Translate(Models.Car source, SaveCar destination)
        {
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }

        public void Translate(SaveCar source, Models.Car destination)
        {
            var now = this.clockService.UtcNow;

            if (destination.Created == DateTimeOffset.MinValue)
            {
                destination.Created = now;
            }

            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Modified = now;
        }
    }
}
