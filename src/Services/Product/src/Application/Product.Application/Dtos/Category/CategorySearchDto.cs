namespace Product.Application.Dtos.Category
{
    public class CategorySearchDto
    {
        public string SearchTerm { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}