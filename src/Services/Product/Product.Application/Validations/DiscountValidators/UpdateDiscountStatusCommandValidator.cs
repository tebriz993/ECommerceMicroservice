using FluentValidation;
using Product.Application.Features.Discount.Commands;

namespace Product.Application.Validations.DiscountValidators
{
    public class UpdateDiscountStatusCommandValidator : AbstractValidator<UpdateDiscountStatusCommand>
    {
        public UpdateDiscountStatusCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("Discount ID is required.");
        }
    }
}