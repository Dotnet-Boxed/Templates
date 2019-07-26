namespace ApiTemplate.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Repositories;
    using Microsoft.AspNetCore.Mvc;

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
