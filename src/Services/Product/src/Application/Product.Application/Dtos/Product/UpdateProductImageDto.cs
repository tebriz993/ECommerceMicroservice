namespace Product.Application.Dtos.Product
{
    public class UpdateProductImageDto
    {
        public Guid? Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMainImage { get; set; }
        public bool ToDelete { get; set; } = false;
    }
}