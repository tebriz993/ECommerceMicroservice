using System.Collections.Generic;
using Product.Application.Dtos.Product;

namespace Product.Application.Dtos.Category
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public IReadOnlyList<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
