using FluentValidation;
using Product.Application.Dtos.Base;
using Product.Application.Dtos.Discount;  // For DiscountBaseDto
using Product.Application.Features.Discount.Commands;

namespace Product.Application.Validations.DiscountValidators
{
    public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Discount ID is required for an update.");
            RuleFor(v => v.DiscountDto)
                .NotNull()
                .SetValidator(new UpdateDiscountDtoValidator()); 
        }
    }

}