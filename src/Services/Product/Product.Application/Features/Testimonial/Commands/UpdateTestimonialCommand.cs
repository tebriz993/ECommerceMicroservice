using MediatR;
using Product.Application.Dtos.Testimonial;
using System;

namespace Product.Application.Features.Testimonial.Commands
{
    public class UpdateTestimonialCommand : IRequest
    {
        public Guid Id { get; set; }
        public UpdateTestimonialDto UpdateDto { get; set; }
    }
}