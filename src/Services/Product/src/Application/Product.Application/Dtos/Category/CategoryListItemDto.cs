namespace Product.Application.Dtos.Category
{
    public class CategoryListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int ProductCount { get; set; }
    }
}