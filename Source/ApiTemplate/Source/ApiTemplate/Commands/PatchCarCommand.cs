namespace ApiTemplate.Commands;

using ApiTemplate.Repositories;
using ApiTemplate.ViewModels;
using Boxed.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class PatchCarCommand
{
    private readonly IActionContextAccessor actionContextAccessor;
    private readonly ICarRepository carRepository;
    private readonly IMapper<Models.Car, Car> carToCarMapper;
    private readonly IMapper<Models.Car, SaveCar> carToSaveCarMapper;
    private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;
    private readonly IValidator<SaveCar> saveCarValidator;

    public PatchCarCommand(
        IActionContextAccessor actionContextAccessor,
        ICarRepository carRepository,
        IMapper<Models.Car, Car> carToCarMapper,
        IMapper<Models.Car, SaveCar> carToSaveCarMapper,
        IMapper<SaveCar, Models.Car> saveCarToCarMapper,
        IValidator<SaveCar> saveCarValidator)
    {
        this.actionContextAccessor = actionContextAccessor;
        this.carRepository = carRepository;
        this.carToCarMapper = carToCarMapper;
        this.carToSaveCarMapper = carToSaveCarMapper;
        this.saveCarToCarMapper = saveCarToCarMapper;
        this.saveCarValidator = saveCarValidator;
    }

    public async Task<IActionResult> ExecuteAsync(
        int carId,
        JsonPatchDocument<SaveCar> patch,
        CancellationToken cancellationToken)
    {
        var car = await this.carRepository.GetAsync(carId, cancellationToken).ConfigureAwait(false);
        if (car is null)
        {
            return new NotFoundResult();
        }

        var saveCar = this.carToSaveCarMapper.Map(car);
        var modelState = this.actionContextAccessor.ActionContext!.ModelState;
        patch.ApplyTo(saveCar, modelState);
        var validationResult = this.saveCarValidator.Validate(saveCar);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(modelState);
            return new BadRequestObjectResult(new ValidationProblemDetails(modelState));
        }

        this.saveCarToCarMapper.Map(saveCar, car);
        await this.carRepository.UpdateAsync(car, cancellationToken).ConfigureAwait(false);
        var carViewModel = this.carToCarMapper.Map(car);

        return new OkObjectResult(carViewModel);
    }
}
