using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class UpdateVariantStockCommandValidator : AbstractValidator<UpdateVariantStockCommand>
    {
        public UpdateVariantStockCommandValidator()
        {
            RuleFor(v => v.VariantId).NotEmpty();

            // QuantityChange sıfır olmamalıdır, çünki bu, heç bir dəyişiklik demək deyil.
            RuleFor(v => v.QuantityChange)
                .NotEqual(0).WithMessage("Quantity change cannot be zero.");
        }
    }
}