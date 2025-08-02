using AutoMapper;
using Product.Application.Dtos.Testimonial;
using Product.Domain.Entities;

namespace Product.Application.AutoMapper
{
    public class TestimonialsProfile : Profile
    {
        public TestimonialsProfile()
        {
            CreateMap<Testimonial, TestimonialDto>();
            CreateMap<CreateTestimonialDto, Testimonial>();
            CreateMap<UpdateTestimonialDto, Testimonial>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}