using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.OldPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Barcode)
                .HasMaxLength(50);

            builder.Property(p => p.Rating)
                .HasColumnType("decimal(2,1)");

            builder.Property(p => p.Weight)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Height)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Width)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Length)
                .HasColumnType("decimal(10,2)");

            // Relationships
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Discounts) // Bir məhsulun çox endirimi ola bilər
                .WithMany(d => d.ApplicableProducts) // Bir endirim çox məhsula aid ola bilər
                .UsingEntity(j => j.ToTable("ProductDiscounts"));
        }
    }
}