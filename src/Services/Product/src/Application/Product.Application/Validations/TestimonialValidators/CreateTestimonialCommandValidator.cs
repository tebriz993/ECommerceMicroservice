using FluentValidation;
using Product.Application.Dtos.Testimonial;
using Product.Application.Features.Testimonial.Commands;

namespace Product.Application.Validations.TestimonialValidators
{
    public class CreateTestimonialCommandValidator : AbstractValidator<CreateTestimonialCommand>
    {
        public CreateTestimonialCommandValidator()
        {
            RuleFor(v => v.CreateDto)
                .NotNull().WithMessage("Testimonial data is required.");

            RuleFor(v => v.CreateDto)
                .SetValidator(new CreateTestimonialDtoValidator());
        }
    }

    public class CreateTestimonialDtoValidator : AbstractValidator<CreateTestimonialDto>
    {
        public CreateTestimonialDtoValidator()
        {
            RuleFor(v => v.ClientName)
                .NotEmpty().WithMessage("Client name is required.")
                .MaximumLength(100).WithMessage("Client name cannot exceed 100 characters.");

            RuleFor(v => v.Comment)
                .NotEmpty().WithMessage("Comment is required.")
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");

            RuleFor(v => v.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(v => v.Profession)
                .MaximumLength(100).WithMessage("Profession cannot exceed 100 characters.");

            RuleFor(v => v.ImageUrl)
                .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters.");
        }
    }
}