namespace ApiTemplate.Commands;

using ApiTemplate.Repositories;
using ApiTemplate.ViewModels;
using Boxed.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class PutCarCommand
{
    private readonly IActionContextAccessor actionContextAccessor;
    private readonly ICarRepository carRepository;
    private readonly IMapper<Models.Car, Car> carToCarMapper;
    private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;
    private readonly IValidator<SaveCar> saveCarValidator;

    public PutCarCommand(
        IActionContextAccessor actionContextAccessor,
        ICarRepository carRepository,
        IMapper<Models.Car, Car> carToCarMapper,
        IMapper<SaveCar, Models.Car> saveCarToCarMapper,
        IValidator<SaveCar> saveCarValidator)
    {
        this.actionContextAccessor = actionContextAccessor;
        this.carRepository = carRepository;
        this.carToCarMapper = carToCarMapper;
        this.saveCarToCarMapper = saveCarToCarMapper;
        this.saveCarValidator = saveCarValidator;
    }

    public async Task<IActionResult> ExecuteAsync(int carId, SaveCar saveCar, CancellationToken cancellationToken)
    {
        var validationResult = this.saveCarValidator.Validate(saveCar);
        if (!validationResult.IsValid)
        {
            var modelState = this.actionContextAccessor.ActionContext!.ModelState;
            validationResult.AddToModelState(modelState);
            return new BadRequestObjectResult(new ValidationProblemDetails(modelState));
        }

        var car = await this.carRepository.GetAsync(carId, cancellationToken).ConfigureAwait(false);
        if (car is null)
        {
            return new NotFoundResult();
        }

        this.saveCarToCarMapper.Map(saveCar, car);
        car = await this.carRepository.UpdateAsync(car, cancellationToken).ConfigureAwait(false);
        var carViewModel = this.carToCarMapper.Map(car);

        return new OkObjectResult(carViewModel);
    }
}
