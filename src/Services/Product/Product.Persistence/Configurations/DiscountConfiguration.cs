using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.DiscountValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Enum-u verilənlər bazasında rəqəm (0, 1) yerinə string ("Percentage", "FixedAmount")
            // olaraq saxlamaq daha oxunaqlıdır və gələcəkdə yeni tiplər əlavə etməyi asanlaşdırır.
            builder.Property(d => d.DiscountType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(d => d.StartDate)
                .IsRequired();

            builder.Property(d => d.EndDate)
                .IsRequired();

            builder.Property(d => d.IsActive)
                .IsRequired();

            // Many-to-Many əlaqələrini burada da təyin edə bilərik,
            // amma aydınlıq üçün onları digər konfiqurasiya fayllarında təyin etmək daha yaxşıdır.
            // EF Core bir tərəfdə təyin olunmasını kifayət hesab edir.
        }
    }
}