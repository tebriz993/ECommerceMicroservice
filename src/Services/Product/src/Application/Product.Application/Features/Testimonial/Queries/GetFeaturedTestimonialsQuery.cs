using MediatR;
using Product.Application.Dtos.Testimonial;
using System.Collections.Generic;

namespace Product.Application.Features.Testimonial.Queries
{
    public class GetFeaturedTestimonialsQuery : IRequest<IReadOnlyList<TestimonialDto>>
    {
        public int Count { get; set; } = 3;
    }
}