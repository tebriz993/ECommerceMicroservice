using FluentValidation;
using Product.Application.Features.Categories.Commands;

namespace Product.Application.Validations.CategoryValidations
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.UpdateCategoryDto.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}