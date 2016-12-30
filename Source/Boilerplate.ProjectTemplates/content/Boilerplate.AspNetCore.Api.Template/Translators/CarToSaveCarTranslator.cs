namespace MvcBoilerplate.Translators
{
    using Framework;
    using MvcBoilerplate.ViewModels;

    public class CarToSaveCarTranslator :  ITranslator<Models.Car, SaveCar>, ITranslator<SaveCar, Models.Car>
    {
        public void Translate(Models.Car source, SaveCar destination)
        {
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }

        public void Translate(SaveCar source, Models.Car destination)
        {
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }
    }
}
