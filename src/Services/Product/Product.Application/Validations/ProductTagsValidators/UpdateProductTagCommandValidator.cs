using FluentValidation;
using Product.Application.Features.ProductTags.Commands;

namespace Product.Application.Validations.ProductTagsValidators
{
    public class UpdateProductTagCommandValidator : AbstractValidator<UpdateProductTagCommand>
    {
        public UpdateProductTagCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.UpdateTagDto).NotNull();
            RuleFor(c => c.UpdateTagDto.Name)
                .NotEmpty().WithMessage("Tag name is required.")
                .MaximumLength(50).WithMessage("Tag name cannot exceed 50 characters.");
        }
    }
}