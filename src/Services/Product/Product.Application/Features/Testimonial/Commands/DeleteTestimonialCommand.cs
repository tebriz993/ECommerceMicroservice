using MediatR;
using System;

namespace Product.Application.Features.Testimonial.Commands
{
    public class DeleteTestimonialCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}