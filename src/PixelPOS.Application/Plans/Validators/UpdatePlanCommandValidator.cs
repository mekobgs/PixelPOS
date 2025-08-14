using FluentValidation;
using PixelPOS.Application.Plans.Commands;

namespace PixelPOS.Application.Plans.Validators;

public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
{
    public UpdatePlanCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().WithMessage("Plan name is required.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
        RuleFor(x => x.MaxUsers).GreaterThan(0).When(x => x.MaxUsers.HasValue);
        RuleFor(x => x.MaxBranches).GreaterThan(0).When(x => x.MaxBranches.HasValue);
    }
}
