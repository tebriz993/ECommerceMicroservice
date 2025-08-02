using AutoMapper;
using MediatR;
using Product.Application.Dtos.Category;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Queries;

public class SearchCategoriesQueryHandler : IRequestHandler<SearchCategoriesQuery, IReadOnlyList<CategoryListItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CategoryListItemDto>> Handle(SearchCategoriesQuery request, CancellationToken cancellationToken)
    {
        // 1. Repozitoridəki yeni axtarış metodumuzu çağırırıq.
        var categories = await _unitOfWork.CategoryRepository.SearchCategoriesAsync(
            request.SearchTerm,
            request.IsActive,
            request.MaxItems
        );

        // 2. Bazadan gələn `Category` entity-lərini `CategoryListItemDto`-lara çeviririk.
        // `CategoryListItemDto` yalnız UI-da göstərmək üçün lazımi minimal məlumatları saxlayır.
        return _mapper.Map<IReadOnlyList<CategoryListItemDto>>(categories);
    }
}