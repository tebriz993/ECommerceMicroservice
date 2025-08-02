using AutoMapper;
using MediatR;
using Product.Application.Dtos.Category;
using Product.Application.Interfaces;
using Product.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Queries
{

    public class GetCategoriesByPageQueryHandler : IRequestHandler<GetCategoriesByPageQuery, PagedResponse<IReadOnlyList<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoriesByPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IReadOnlyList<CategoryDto>>> Handle(GetCategoriesByPageQuery request, CancellationToken cancellationToken)
        {
            // 1. Repozitoridən səhifələnmiş datanı və ümumi sayı alırıq.
            var (categories, totalCount) = await _unitOfWork.CategoryRepository.GetCategoriesByPageAsync(request);

            // 2. Nəticəni DTO-ya çeviririk.
            var categoryDtos = _mapper.Map<IReadOnlyList<CategoryDto>>(categories);

            // 3. PagedResponse obyektini yaradıb qaytarırıq.
            return new PagedResponse<IReadOnlyList<CategoryDto>>(categoryDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}