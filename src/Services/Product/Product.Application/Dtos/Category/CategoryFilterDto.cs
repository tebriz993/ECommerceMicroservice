namespace Product.Application.Dtos.Category
{
    public class CategoryFilterDto
    {
        public bool IncludeInactive { get; set; } = false;
        public bool IncludeEmpty { get; set; } = false;
        public int? Level { get; set; }
    }
}