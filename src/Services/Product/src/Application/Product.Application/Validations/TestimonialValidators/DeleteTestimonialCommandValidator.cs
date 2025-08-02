using FluentValidation;
using Product.Application.Features.Testimonial.Commands;

namespace Product.Application.Validations.TestimonialValidators
{
    public class DeleteTestimonialCommandValidator : AbstractValidator<DeleteTestimonialCommand>
    {
        public DeleteTestimonialCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Testimonial ID is required to delete.");
        }
    }
}