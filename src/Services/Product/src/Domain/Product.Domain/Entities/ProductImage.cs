using Product.Domain.Common;

namespace Product.Domain.Entities
{

    public class ProductImage : EntityBase
    {
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool IsMainImage { get; set; } = false;
    }
}