using System.Collections.Generic;

namespace Product.Application.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public IReadOnlyList<CreateProductImageDto> Images { get; set; } = new List<CreateProductImageDto>();
        public IReadOnlyList<CreateProductVariantDto> Variants { get; set; } = new List<CreateProductVariantDto>();
        public IReadOnlyList<Guid> TagIds { get; set; } = new List<Guid>();
    }
}