using FluentValidation;
using Product.Application.Features.Categories.Commands;

namespace Product.Application.Validations.CategoryValidations
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.CreateCategoryDto.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(c => c.CreateCategoryDto.Description)
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
        }
    }
}
