using System.Collections.Generic;
using Product.Application.Dtos.Product;

namespace Product.Application.Dtos.Category
{
    public class CategoryDetailsDto : CategoryDto
    {
        public IReadOnlyList<ProductDto> FeaturedProducts { get; set; } = new List<ProductDto>();
        public IReadOnlyList<ProductDto> LatestProducts { get; set; } = new List<ProductDto>();
        public IReadOnlyList<CategoryDto> SiblingCategories { get; set; } = new List<CategoryDto>();
    }
}