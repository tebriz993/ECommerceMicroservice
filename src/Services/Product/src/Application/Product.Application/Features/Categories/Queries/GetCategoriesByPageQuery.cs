using MediatR;
using Product.Application.Dtos.Category;
using Product.Application.Wrappers;
using System.Collections.Generic; // IReadOnlyList üçün

namespace Product.Application.Features.Categories.Queries
{

    // DÜZƏLİŞ BURADADIR: : IRequest<...> hissəsini əlavə edirik.
    // Bu, MediatR-a deyir ki, bu sorğunun cavabı PagedResponse<IReadOnlyList<CategoryDto>> tipində olacaq.
    public class GetCategoriesByPageQuery : IRequest<PagedResponse<IReadOnlyList<CategoryDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
    }
}