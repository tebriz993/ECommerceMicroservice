using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(pv => pv.Id);

            builder.Property(pv => pv.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pv => pv.Value)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pv => pv.PriceAdjustment)
                .HasColumnType("decimal(18,2)");

            builder.Property(pv => pv.SKU)
                .HasMaxLength(50);

            // Relationship
            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}