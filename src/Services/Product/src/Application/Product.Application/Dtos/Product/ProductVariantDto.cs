namespace Product.Application.Dtos.Product
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal PriceAdjustment { get; set; }
        public int Quantity { get; set; }
    }
}