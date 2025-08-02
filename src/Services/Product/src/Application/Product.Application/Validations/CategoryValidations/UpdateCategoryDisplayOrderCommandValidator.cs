using FluentValidation;

namespace Product.Application.Features.Categories.Commands;

public class UpdateCategoryDisplayOrderCommandValidator : AbstractValidator<UpdateCategoryDisplayOrderCommand>
{
    public UpdateCategoryDisplayOrderCommandValidator()
    {
        RuleFor(c => c.CategoryOrders)
            .NotEmpty().WithMessage("Category orders cannot be empty.")
            .Must(orders => orders.Keys.All(key => key != Guid.Empty))
            .WithMessage("Invalid Category ID found.");
    }
}