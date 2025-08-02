using System.Collections.Generic;

namespace Product.Application.Dtos.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsFeatured { get; set; }
        public IReadOnlyList<UpdateProductImageDto> Images { get; set; } = new List<UpdateProductImageDto>();
    }
}