using MediatR;
using Product.Application.Dtos.Testimonial;
using Product.Application.Wrappers;
using System.Collections.Generic;

namespace Product.Application.Features.Testimonial.Queries
{
    public class GetTestimonialsByPageQuery : IRequest<PagedResponse<IReadOnlyList<TestimonialDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}