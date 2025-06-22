using FluentValidation;
using Product.Application.Features.ProductTags.Commands;

namespace Product.Application.Validations.ProductTagsValidators
{
    public class CreateProductTagCommandValidator : AbstractValidator<CreateProductTagCommand>
    {
        public CreateProductTagCommandValidator()
        {
            RuleFor(c => c.CreateTagDto).NotNull();
            RuleFor(c => c.CreateTagDto.Name)
                .NotEmpty().WithMessage("Tag name is required.")
                .MaximumLength(50).WithMessage("Tag name cannot exceed 50 characters.");
        }
    }
}