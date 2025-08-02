using FluentValidation;
using Product.Application.Features.Categories.Commands;

namespace Product.Application.Validations.CategoryValidations
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}