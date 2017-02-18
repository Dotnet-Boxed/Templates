namespace ApiTemplate.Commands
{
    using System.Threading.Tasks;
    using Boilerplate;
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

        public async Task<IActionResult> ExecuteAsync(PageOptions pageOptions)
        {
            var cars = await this.carRepository.GetPage(pageOptions.Page, pageOptions.Count);
            if (cars == null)
            {
                return new NotFoundResult();
            }

            var totalPages = await this.carRepository.GetTotalPages(pageOptions.Page, pageOptions.Count);
            var carViewModels = this.carTranslator.TranslateList(cars);
            var page = new PageResult<Car>()
            {
                Count = pageOptions.Count,
                Items = carViewModels,
                Page = pageOptions.Page,
                Total = totalPages
            };
            return new OkObjectResult(page);
        }
    }
}
