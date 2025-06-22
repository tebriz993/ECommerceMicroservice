using FluentValidation;
using Product.Application.Dtos.Testimonial;
using Product.Application.Features.Testimonial.Commands;

namespace Product.Application.Validations.TestimonialValidators
{
    public class UpdateTestimonialCommandValidator : AbstractValidator<UpdateTestimonialCommand>
    {
        public UpdateTestimonialCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Testimonial ID is required for an update.");

            RuleFor(v => v.UpdateDto)
                .NotNull().WithMessage("Testimonial data is required.");

            RuleFor(v => v.UpdateDto)
                .SetValidator(new UpdateTestimonialDtoValidator());
        }
    }

    public class UpdateTestimonialDtoValidator : AbstractValidator<UpdateTestimonialDto>
    {
        public UpdateTestimonialDtoValidator()
        {
            RuleFor(v => v.ClientName).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Comment).NotEmpty().MaximumLength(1000);
            RuleFor(v => v.Rating).InclusiveBetween(1, 5);
            RuleFor(v => v.Profession).MaximumLength(100);
            RuleFor(v => v.ImageUrl).MaximumLength(500);
        }
    }
}