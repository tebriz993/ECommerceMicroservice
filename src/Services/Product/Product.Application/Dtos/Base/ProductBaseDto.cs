namespace Product.Application.Dtos
{
    public abstract class ProductBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string SKU { get; set; }
    }
}