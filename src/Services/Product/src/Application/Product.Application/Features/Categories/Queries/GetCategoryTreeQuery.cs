using MediatR;
using Product.Application.Dtos.Category;

namespace Product.Application.Features.Categories.Queries
{

    // Bütün kateqoriyaları ağac şəklində qaytaracaq.
    public class GetCategoryTreeQuery : IRequest<IReadOnlyList<CategoryTreeDto>> { }
}