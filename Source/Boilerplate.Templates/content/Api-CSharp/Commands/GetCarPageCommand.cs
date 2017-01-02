namespace ApiTemplate.Commands
{
    using System.Threading.Tasks;
    using Framework;
    using Microsoft.AspNetCore.Mvc;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;

    public class GetCarPageCommand : IGetCarPageCommand
    {
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carTranslator;

        public GetCarPageCommand(
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carTranslator)
        {
            this.carRepository = carRepository;
            this.carTranslator = carTranslator;
        }

        public async Task<IActionResult> ExecuteAsync(PageRequest pageRequest)
        {
            var cars = await this.carRepository.GetPage(pageRequest.Page, pageRequest.Count);
            if (cars.Count == 0)
            {
                return new NotFoundResult();
            }

            var totalPages = await this.carRepository.GetTotalPages(pageRequest.Page, pageRequest.Count);
            var carViewModels = this.carTranslator.TranslateList(cars);
            var page = new Page<Car>()
            {
                Count = pageRequest.Count,
                Items = carViewModels,
                PageNumber = pageRequest.Page,
                TotalPages = totalPages
            };
            return new OkObjectResult(page);
        }
    }
}
