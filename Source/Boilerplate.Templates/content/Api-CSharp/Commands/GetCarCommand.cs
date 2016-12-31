namespace MvcBoilerplate.Commands
{
    using System.Threading.Tasks;
    using Framework;
    using Microsoft.AspNetCore.Mvc;
    using MvcBoilerplate.Repositories;
    using MvcBoilerplate.ViewModels;

    public class GetCarCommand : IGetCarCommand
    {
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carTranslator;

        public GetCarCommand(
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carTranslator)
        {
            this.carRepository = carRepository;
            this.carTranslator = carTranslator;
        }

        public async Task<IActionResult> ExecuteAsync(int carId)
        {
            var car = await this.carRepository.Get(carId);
            if (car == null)
            {
                return new NotFoundResult();
            }

            var carViewModel = this.carTranslator.Translate(car);
            return new OkObjectResult(carViewModel);
        }
    }
}
