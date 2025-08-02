using AutoMapper;
using MediatR;
using Product.Application.Dtos.Testimonial;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Testimonial.Queries
{
    public class GetFeaturedTestimonialsQueryHandler : IRequestHandler<GetFeaturedTestimonialsQuery, IReadOnlyList<TestimonialDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeaturedTestimonialsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TestimonialDto>> Handle(GetFeaturedTestimonialsQuery request, CancellationToken cancellationToken)
        {
            var testimonials = await _unitOfWork.TestimonialRepository.GetFeaturedTestimonialsAsync(request.Count);
            return _mapper.Map<IReadOnlyList<TestimonialDto>>(testimonials);
        }
    }
}