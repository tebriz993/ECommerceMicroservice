using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommand>
    {
        public DeleteProductImageCommandValidator()
        {
            RuleFor(p => p.ImageId).NotEmpty().WithMessage("ProductImage ID is required to delete.");
        }
    }
}