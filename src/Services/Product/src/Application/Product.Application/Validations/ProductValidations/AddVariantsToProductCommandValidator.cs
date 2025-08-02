using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class AddVariantsToProductCommandValidator : AbstractValidator<AddVariantsToProductCommand>
    {
        public AddVariantsToProductCommandValidator()
        {
            RuleFor(v => v.ProductId).NotEmpty();

            RuleFor(v => v.Variants)
                .NotEmpty().WithMessage("At least one variant must be provided.");

            RuleForEach(v => v.Variants).ChildRules(variant =>
            {
                variant.RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
                variant.RuleFor(dto => dto.Value).NotEmpty().MaximumLength(100);
                variant.RuleFor(dto => dto.Quantity).GreaterThanOrEqualTo(0);
            });
        }
    }
}