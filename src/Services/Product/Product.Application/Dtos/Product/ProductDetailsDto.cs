using System.Collections.Generic;

namespace Product.Application.Dtos.Product
{
    public class ProductDetailsDto : ProductDto
    {
        public IReadOnlyList<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
        public IReadOnlyList<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();
        public IReadOnlyList<ProductTagDto> Tags { get; set; } = new List<ProductTagDto>();
        public IReadOnlyList<RelatedProductDto> RelatedProducts { get; set; } = new List<RelatedProductDto>();
    }
}
