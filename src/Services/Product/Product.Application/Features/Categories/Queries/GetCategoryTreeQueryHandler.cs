using AutoMapper;
using MediatR;
using Product.Application.Dtos.Category;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Queries
{

    public class GetCategoryTreeQueryHandler : IRequestHandler<GetCategoryTreeQuery, IReadOnlyList<CategoryTreeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryTreeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryTreeDto>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryTreeDto>>(allCategories);

            // Düz siyahıdan ağac strukturu yaradırıq.
            var categoryMap = categoryDtos.ToDictionary(c => c.Id);
            var tree = new List<CategoryTreeDto>();

            foreach (var category in categoryDtos)
            {
                if (category.ParentCategoryId.HasValue && categoryMap.TryGetValue(category.ParentCategoryId.Value, out var parent))
                {
                    // `as List<...>` cast-ı lazımdır, çünki IReadOnlyList-ə əlavə etmək olmur.
                    (parent.Children as List<CategoryTreeDto>)?.Add(category);
                }
                else
                {
                    // Ana kateqoriyadırsa, birbaşa ağaca əlavə edirik.
                    tree.Add(category);
                }
            }
            return tree;
        }
    }
}