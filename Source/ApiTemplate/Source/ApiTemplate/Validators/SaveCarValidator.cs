namespace ApiTemplate.Validators;

using ApiTemplate.ViewModels;
using FluentValidation;

public class SaveCarValidator : AbstractValidator<SaveCar>
{
    public SaveCarValidator()
    {
        this.RuleFor(static x => x.Cylinders).InclusiveBetween(1, 20);
        this.RuleFor(static x => x.Make).NotEmpty();
        this.RuleFor(static x => x.Model).NotEmpty();
    }
}
