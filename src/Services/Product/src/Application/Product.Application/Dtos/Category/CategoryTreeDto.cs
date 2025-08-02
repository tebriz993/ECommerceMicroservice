using System.Collections.Generic;

namespace Product.Application.Dtos.Category
{
    public class CategoryTreeDto : CategoryDto
    {
        public IReadOnlyList<CategoryTreeDto> Children { get; set; } = new List<CategoryTreeDto>();
    }
}