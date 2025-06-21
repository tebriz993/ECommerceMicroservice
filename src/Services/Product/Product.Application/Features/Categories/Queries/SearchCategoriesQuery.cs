using MediatR;
using Product.Application.Dtos.Category;

namespace Product.Application.Features.Categories.Queries
{
    public class SearchCategoriesQuery : IRequest<IReadOnlyList<CategoryListItemDto>>
    {
        public string SearchTerm { get; set; }
        public bool? IsActive { get; set; } // Aktiv və ya passivlərə görə filter
        public int? MaxItems { get; set; } = 20; // Auto-complete üçün nəticə limiti
    }
}