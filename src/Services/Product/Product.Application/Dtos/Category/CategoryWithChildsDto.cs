using System.Collections.Generic;

namespace Product.Application.Dtos.Category
{
    public class CategoryWithChildsDto : CategoryDto
    {
        public IReadOnlyList<CategoryDto> ChildCategories { get; set; } = new List<CategoryDto>();
    }
}