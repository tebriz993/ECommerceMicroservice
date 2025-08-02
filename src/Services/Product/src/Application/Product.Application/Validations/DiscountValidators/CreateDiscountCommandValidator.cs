using FluentValidation;
using Product.Application.Dtos.Discount; // For CreateDiscountDto
using Product.Application.Features.Discount.Commands;

namespace Product.Application.Validations.DiscountValidators
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            // CORRECTED: Apply the rules to the nested DTO
            RuleFor(v => v.DiscountDto)
                .NotNull()
                .SetValidator(new CreateDiscountDtoValidator());
        }
    }

    
}