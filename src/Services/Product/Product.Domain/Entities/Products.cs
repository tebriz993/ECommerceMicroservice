using Product.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{

    // Product.cs
    public class Products : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public int QuantityInStock { get; set; } = 0;
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public decimal Rating { get; set; } = 0.0m;
        public int TotalReviews { get; set; } = 0;

        // Relationships
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ProductTag> Tags { get; set; } = new List<ProductTag>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public ICollection<RelatedProduct> RelatedProducts { get; set; } = new List<RelatedProduct>();
        public ICollection<RelatedProduct> RelatedToProducts { get; set; } = new List<RelatedProduct>();
    }
}