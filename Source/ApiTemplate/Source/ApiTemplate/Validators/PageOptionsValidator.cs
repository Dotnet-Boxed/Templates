namespace ApiTemplate.Validators;

using ApiTemplate.ViewModels;
using FluentValidation;

public class PageOptionsValidator : AbstractValidator<PageOptions>
{
    public PageOptionsValidator()
    {
        this.RuleFor(static x => x.First).InclusiveBetween(1, 20);
        this.RuleFor(static x => x.Last).InclusiveBetween(1, 20);
    }
}
