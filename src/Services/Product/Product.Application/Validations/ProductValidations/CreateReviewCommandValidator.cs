using FluentValidation;
using Product.Application.Features.Reviews.Commands;

namespace Product.Application.Validations.ProductValidations
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(r => r.ReviewDto.ProductId).NotEmpty();
            RuleFor(r => r.ReviewDto.UserName).NotEmpty().MaximumLength(100);
            RuleFor(r => r.ReviewDto.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.ReviewDto.Rating).InclusiveBetween(1, 5);
            RuleFor(r => r.ReviewDto.Comment).NotEmpty().MaximumLength(1000);
        }
    }
}