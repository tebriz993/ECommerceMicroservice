using Product.Application.Dtos.Base;
using System;

namespace Product.Application.Dtos.Testimonial
{
    public class TestimonialDto : TestimonialBaseDto
    {
        public Guid Id { get; set; }
    }
}