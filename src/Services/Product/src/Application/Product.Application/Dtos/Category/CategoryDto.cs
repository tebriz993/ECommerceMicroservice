using Product.Application.Dtos.Base;

namespace Product.Application.Dtos.Category
{
    public class CategoryDto : CategoryBaseDto
    {
        public Guid? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
    }
}