using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            // DTO-nun içindəki obyektə qayda yazmaq üçün RuleFor-u zəncirvari istifadə edirik.
            RuleFor(p => p.CreateProductDto).NotNull();

            RuleFor(p => p.CreateProductDto.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(150).WithMessage("Product name cannot exceed 150 characters.");

            RuleFor(p => p.CreateProductDto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.CreateProductDto.CategoryId)
                .NotEmpty().WithMessage("A category must be selected.");

            RuleFor(p => p.CreateProductDto.Images)
                .Must(images => images == null || !images.Any() || images.Count(i => i.IsMainImage) == 1)
                .WithMessage("A product must have exactly one main image if images are provided.");
        }
    }
}