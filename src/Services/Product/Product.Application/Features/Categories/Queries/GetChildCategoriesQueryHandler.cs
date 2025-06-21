using AutoMapper;
using MediatR;
using Product.Application.Dtos.Category;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Queries
{

    public class GetChildCategoriesQueryHandler : IRequestHandler<GetChildCategoriesQuery, IReadOnlyList<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChildCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> Handle(GetChildCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Artıq bu metod repozitoridə mövcuddur və düzgün işləyir.
            var categories = await _unitOfWork.CategoryRepository
                .GetChildCategoriesAsync(request.ParentId, request.TrackChanges);

            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }
    }
}