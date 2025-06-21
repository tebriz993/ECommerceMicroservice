namespace Product.Application.Dtos.Product
{
    public class CreateProductVariantDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal PriceAdjustment { get; set; } = 0;
        public int Quantity { get; set; } = 0;
    }
}