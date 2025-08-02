namespace Product.Application.Dtos.Product
{
    public class ProductDto : ProductBaseDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsFeatured { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
    }
}