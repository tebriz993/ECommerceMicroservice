namespace Product.Application.Dtos.Product
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsMainImage { get; set; }
    }
}