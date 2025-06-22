using FluentValidation;
using Product.Application.Features.Discount.Commands;

namespace Product.Application.Validations.DiscountValidators
{
    public class DeleteDiscountCommandValidator : AbstractValidator<DeleteDiscountCommand>
    {
        public DeleteDiscountCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Discount ID is required to delete.");
        }
    }
}