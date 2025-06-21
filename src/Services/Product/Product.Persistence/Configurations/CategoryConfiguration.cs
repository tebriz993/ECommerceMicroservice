using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Description)
                .HasMaxLength(255);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(255);

            // Self-referencing relationship
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Discounts) // Bir kateqoriyanın çox endirimi ola bilər
                .WithMany(d => d.ApplicableCategories) // Bir endirim çox kateqoriyaya aid ola bilər
                .UsingEntity(j => j.ToTable("CategoryDiscounts"));
        }
    }
}