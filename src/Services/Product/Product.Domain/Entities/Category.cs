using Product.Domain.Common;

namespace Product.Domain.Entities
{

    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        // Self-referencing relationship
        public Guid? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; } = new List<Category>();

        // Products relationship
        public ICollection<Products> Products { get; set; } = new List<Products>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}