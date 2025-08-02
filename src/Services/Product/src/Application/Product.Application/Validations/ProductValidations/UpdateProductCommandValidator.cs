using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Product ID is required for an update operation.");

            RuleFor(p => p.UpdateProductDto).NotNull();

            RuleFor(p => p.UpdateProductDto.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(150).WithMessage("Product name cannot exceed 150 characters.");

            RuleFor(p => p.UpdateProductDto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}