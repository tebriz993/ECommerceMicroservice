using AutoMapper;
using MediatR;
using Product.Application.Dtos.Category;
using Product.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Queries
{

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryWithProductsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryWithProductsDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            // DİQQƏT: Kateqoriyanı məhsulları ilə birlikdə gətirmək üçün spesifik repozitori metodu lazımdır.
            // ICategoryRepository-dəki GetCategoriesWithProductsAsync bunu edir, amma ID ilə yox.
            // Gəlin yeni bir metod təsəvvür edək: GetCategoryWithProductsByIdAsync(id)

            // Təsəvvür edək ki, ICategoryRepository-ə belə bir metod əlavə etdik:
            // Task<Category> GetCategoryWithProductsByIdAsync(Guid id, bool trackChanges = false);
            // Və onun implementasiyasını CategoryRepository-də yazdıq.

            var category = await _unitOfWork.CategoryRepository.GetCategoryWithProductsByIdAsync(request.Id, request.TrackChanges);

            if (category is null)
            {
                throw new KeyNotFoundException($"Category with id {request.Id} not found.");
            }

            return _mapper.Map<CategoryWithProductsDto>(category);
        }
    }
}