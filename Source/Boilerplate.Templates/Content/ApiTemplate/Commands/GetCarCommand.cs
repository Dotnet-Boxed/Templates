namespace ApiTemplate.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;
    using Boilerplate.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class GetCarCommand : IGetCarCommand
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICarRepository carRepository;
        private readonly IMapper<Models.Car, Car> carMapper;

        public GetCarCommand(
            IHttpContextAccessor httpContextAccessor,
            ICarRepository carRepository,
            IMapper<Models.Car, Car> carMapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.carRepository = carRepository;
            this.carMapper = carMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int carId, CancellationToken cancellationToken)
        {
            var car = await this.carRepository.Get(carId, cancellationToken);
            if (car == null)
            {
                return new NotFoundResult();
            }

            // You could use the filter instead of this code. This lets you short-cut the response
            // if you are doing more async work after this to get more data perhaps.
            var httpContext = this.httpContextAccessor.HttpContext;
            if (car.HasBeenModified(httpContext.Request))
            {
                return new StatusCodeResult(StatusCodes.Status304NotModified);
            }
            else
            {
                car.SetModifiedHttpHeaders(httpContext.Response);
            }

            var carViewModel = this.carMapper.Map(car);
            return new OkObjectResult(carViewModel);
        }
    }
}
