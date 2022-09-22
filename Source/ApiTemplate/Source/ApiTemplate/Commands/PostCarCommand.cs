namespace ApiTemplate.Commands;

using ApiTemplate.Constants;
using ApiTemplate.Repositories;
using ApiTemplate.ViewModels;
using Boxed.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class PostCarCommand
{
    private readonly IActionContextAccessor actionContextAccessor;
    private readonly ICarRepository carRepository;
    private readonly IMapper<Models.Car, Car> carToCarMapper;
    private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;
    private readonly IValidator<SaveCar> saveCarValidator;

    public PostCarCommand(
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

    public async Task<IActionResult> ExecuteAsync(SaveCar saveCar, CancellationToken cancellationToken)
    {
        var validationResult = this.saveCarValidator.Validate(saveCar);
        if (!validationResult.IsValid)
        {
            var modelState = this.actionContextAccessor.ActionContext!.ModelState;
            validationResult.AddToModelState(modelState);
            return new BadRequestObjectResult(new ValidationProblemDetails(modelState));
        }

        var car = this.saveCarToCarMapper.Map(saveCar);
        car = await this.carRepository.AddAsync(car, cancellationToken).ConfigureAwait(false);
        var carViewModel = this.carToCarMapper.Map(car);

        return new CreatedAtRouteResult(
            CarsControllerRoute.GetCar,
            new { carId = carViewModel.CarId },
            carViewModel);
    }
}
