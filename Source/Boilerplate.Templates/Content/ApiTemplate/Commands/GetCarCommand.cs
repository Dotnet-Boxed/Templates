namespace ApiTemplate.Commands
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;

    public class GetCarCommand : IGetCarCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carTranslator;

        public GetCarCommand(
            IActionContextAccessor actionContextAccessor,
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carTranslator)
        {
            this.actionContextAccessor = actionContextAccessor;
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

            var httpContext = this.actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out StringValues stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out DateTimeOffset modifiedSince) &&
                    (modifiedSince >= car.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var carViewModel = this.carTranslator.Translate(car);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, car.Modified.ToString("R"));
            return new OkObjectResult(carViewModel);
        }
    }
}
