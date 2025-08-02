using MediatR;
using Product.Application.Dtos.Testimonial;
using System;

namespace Product.Application.Features.Testimonial.Commands
{
    public class CreateTestimonialCommand : IRequest<Guid>
    {
        public CreateTestimonialDto CreateDto { get; set; }
    }
}