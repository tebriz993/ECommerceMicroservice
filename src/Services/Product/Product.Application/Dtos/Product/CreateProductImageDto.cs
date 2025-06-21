namespace Product.Application.Dtos.Product
{
    public class CreateProductImageDto
    {
        public string ImageUrl { get; set; }
        public bool IsMainImage { get; set; } = false;
    }
}