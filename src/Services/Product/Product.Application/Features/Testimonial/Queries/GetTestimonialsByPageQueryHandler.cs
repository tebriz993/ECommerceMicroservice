using AutoMapper;
using MediatR;
using Product.Application.Dtos.Testimonial;
using Product.Application.Interfaces;
using Product.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Testimonial.Queries
{
    public class GetTestimonialsByPageQueryHandler : IRequestHandler<GetTestimonialsByPageQuery, PagedResponse<IReadOnlyList<TestimonialDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTestimonialsByPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IReadOnlyList<TestimonialDto>>> Handle(GetTestimonialsByPageQuery request, CancellationToken cancellationToken)
        {
            var (testimonials, totalCount) = await _unitOfWork.TestimonialRepository.GetTestimonialsByPageAsync(request);

            var testimonialDtos = _mapper.Map<IReadOnlyList<TestimonialDto>>(testimonials);

            return new PagedResponse<IReadOnlyList<TestimonialDto>>(testimonialDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}