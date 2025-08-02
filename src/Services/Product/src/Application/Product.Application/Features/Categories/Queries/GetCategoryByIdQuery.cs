using MediatR;
using Product.Application.Dtos;
using Product.Application.Dtos.Category;

namespace Product.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryWithProductsDto>
    {
        public Guid Id { get; set; }
        public bool TrackChanges { get; set; } = false;
    }
}