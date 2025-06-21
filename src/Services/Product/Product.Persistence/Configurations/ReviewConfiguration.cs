using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Title)
                .HasMaxLength(100);

            builder.Property(r => r.Comment)
                .HasColumnType("nvarchar(max)");

            builder.Property(r => r.Rating)
                .IsRequired();

            // Relationship
            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}