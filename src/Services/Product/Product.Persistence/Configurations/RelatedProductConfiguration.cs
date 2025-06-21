using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configurations
{
    public class RelatedProductConfiguration : IEntityTypeConfiguration<RelatedProduct>
    {
        public void Configure(EntityTypeBuilder<RelatedProduct> builder)
        {
            builder.ToTable("RelatedProducts");

            builder.HasKey(rp => rp.Id);

            // Relationships (self-referencing)
            builder.HasOne(rp => rp.Product)
                .WithMany(p => p.RelatedProducts)
                .HasForeignKey(rp => rp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rp => rp.RelatedToProduct)
                .WithMany(p => p.RelatedToProducts)
                .HasForeignKey(rp => rp.RelatedProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}