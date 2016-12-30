namespace MvcBoilerplate.Translators
{
    using Framework;
    using MvcBoilerplate.ViewModels;

    public class CarToCarTranslator : ITranslator<Models.Car, Car>, ITranslator<Car, Models.Car>
    {
        public void Translate(Models.Car source, Car destination)
        {
            destination.CarId = source.CarId;
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }

        public void Translate(Car source, Models.Car destination)
        {
            destination.CarId = source.CarId;
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }
    }
}
