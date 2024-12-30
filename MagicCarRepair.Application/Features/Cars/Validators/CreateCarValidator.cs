using FluentValidation;
using MagicCarRepair.Application.Features.Cars.Commands.CreateCar;

namespace MagicCarRepair.Application.Features.Cars.Validators;

public class CreateCarValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Year)
            .NotEmpty()
            .GreaterThan(1900)
            .LessThanOrEqualTo(DateTime.Now.Year);

        RuleFor(x => x.LicensePlate)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.OwnerId)
            .NotEmpty();
    }
} 