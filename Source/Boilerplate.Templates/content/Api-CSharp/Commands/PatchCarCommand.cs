namespace ApiTemplate.Commands
{
    using System.Threading.Tasks;
    using Boilerplate;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public class PatchCarCommand : IPatchCarCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IObjectModelValidator objectModelValidator;
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carToCarTranslator;
        private readonly ITranslator<Models.Car, SaveCar> carToSaveCarTranslator;
        private readonly ITranslator<SaveCar, Models.Car> saveCarToCarTranslator;

        public PatchCarCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carToCarTranslator,
            ITranslator<Models.Car, SaveCar> carToSaveCarTranslator,
            ITranslator<SaveCar, Models.Car> saveCarToCarTranslator)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.objectModelValidator = objectModelValidator;
            this.carRepository = carRepository;
            this.carToCarTranslator = carToCarTranslator;
            this.carToSaveCarTranslator = carToSaveCarTranslator;
            this.saveCarToCarTranslator = saveCarToCarTranslator;
        }

        public async Task<IActionResult> ExecuteAsync(int carId, JsonPatchDocument<SaveCar> patch)
        {
            var car = await this.carRepository.Get(carId);
            if (car == null)
            {
                return new NotFoundResult();
            }

            var saveCar = this.carToSaveCarTranslator.Translate(car);
            var modelState = this.actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveCar, modelState);
            this.objectModelValidator.Validate(
                this.actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveCar);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            this.saveCarToCarTranslator.Translate(saveCar, car);
            await this.carRepository.Update(car);
            var carViewModel = this.carToCarTranslator.Translate(car);

            return new OkObjectResult(carViewModel);
        }
    }
}