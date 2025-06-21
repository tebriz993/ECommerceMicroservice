using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configurations
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTags");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Many-to-many relationship
            builder.HasMany(pt => pt.Products)
                .WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductTagMapping",
                    j => j.HasOne<Products>().WithMany().HasForeignKey("ProductId"),
                    j => j.HasOne<ProductTag>().WithMany().HasForeignKey("TagId"),
                    j => j.ToTable("ProductTagMapping"));
        }
    }
}