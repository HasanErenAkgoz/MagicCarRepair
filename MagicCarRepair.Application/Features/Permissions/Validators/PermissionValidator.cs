using FluentValidation;
using MagicCarRepair.Domain.Entities;

namespace MagicCarRepair.Application.Features.Permissions.Validators;

public class PermissionValidator : AbstractValidator<Permission>
{
    public PermissionValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Description)
            .MaximumLength(250);

        RuleFor(p => p.Group)
            .NotEmpty()
            .MaximumLength(50);
    }
} 