using System.Collections.Generic;

namespace Product.Application.Dtos.Category
{
    public class CategoryNavigationDto
    {
        public CategoryDto CurrentCategory { get; set; }
        public IReadOnlyList<CategoryDto> Breadcrumbs { get; set; } = new List<CategoryDto>();
        public IReadOnlyList<CategoryDto> ChildCategories { get; set; } = new List<CategoryDto>();
    }
}