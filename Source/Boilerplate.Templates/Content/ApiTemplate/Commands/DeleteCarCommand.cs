namespace ApiTemplate.Commands
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ApiTemplate.Repositories;
    using System.Threading;

    public class DeleteCarCommand : IDeleteCarCommand
    {
        private readonly ICarRepository carRepository;

        public DeleteCarCommand(ICarRepository carRepository) =>
            this.carRepository = carRepository;

        public async Task<IActionResult> ExecuteAsync(int carId, CancellationToken cancellationToken)
        {
            var car = await this.carRepository.Get(carId, cancellationToken);
            if (car == null)
            {
                return new NotFoundResult();
            }

            await this.carRepository.Delete(car, cancellationToken);

            return new NoContentResult();
        }
    }
}
