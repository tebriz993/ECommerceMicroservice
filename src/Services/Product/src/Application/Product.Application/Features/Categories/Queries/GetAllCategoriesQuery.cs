using MediatR;
using Product.Application.Dtos;
using Product.Application.Dtos.Category;

namespace Product.Application.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>
    {
        public bool TrackChanges { get; set; } = false;
    }
}