using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    /// <summary>
    /// Validates the UpdateProductImageCommand.
    /// </summary>
    public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
    {
        public UpdateProductImageCommandValidator()
        {
            // Əsas Command-ın ID-sini yoxlayırıq.
            RuleFor(c => c.ImageId)
                .NotEmpty().WithMessage("Image ID is required for an update operation.");

            // İç-içə olan DTO-nun null olmamasını yoxlayırıq.
            RuleFor(c => c.UpdateDto)
                .NotNull().WithMessage("Update data cannot be null.");

            // DTO-nun içindəki property-ləri yoxlamaq üçün RuleFor-u zəncirvari istifadə edirik.
            // Bu, DTO null olmadıqda işə düşəcək.
            When(c => c.UpdateDto != null, () =>
            {
                RuleFor(c => c.UpdateDto.ImageUrl)
                    .NotEmpty().WithMessage("Image URL cannot be empty.")
                    .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters.");
            });
        }
    }
}