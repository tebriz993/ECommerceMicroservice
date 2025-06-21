using FluentValidation;

namespace Product.Application.Features.Categories.Commands;

public class UpdateCategoryStatusCommandValidator : AbstractValidator<UpdateCategoryStatusCommand>
{
    public UpdateCategoryStatusCommandValidator()
    {
        // ID-nin boş olmamasını yoxlayırıq.
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        // IsActive üçün xüsusi bir qaydaya ehtiyac yoxdur, çünki bool həmişə true/false olur.
    }
}