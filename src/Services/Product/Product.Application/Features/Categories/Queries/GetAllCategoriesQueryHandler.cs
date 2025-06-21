using AutoMapper;
using MediatR;
using Product.Application.Dtos;
using Product.Application.Dtos.Category;
using Product.Application.Interfaces;

namespace Product.Application.Features.Categories.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository
                .GetCategoriesWithProductsAsync(request.TrackChanges);

            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }
    }
}