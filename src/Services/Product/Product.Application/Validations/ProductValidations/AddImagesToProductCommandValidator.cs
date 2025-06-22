using FluentValidation;
using Product.Application.Features.Products.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class AddImagesToProductCommandValidator : AbstractValidator<AddImagesToProductCommand>
    {
        public AddImagesToProductCommandValidator()
        {
            RuleFor(c => c.ProductId).NotEmpty();
            RuleFor(c => c.Images).NotEmpty().WithMessage("At least one image must be provided.");
            RuleForEach(c => c.Images).ChildRules(image =>
            {
                image.RuleFor(i => i.ImageUrl).NotEmpty().MaximumLength(500);
            });
        }
    }
}