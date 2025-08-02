using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Persistence.Configurations // Sizin layihə strukturunuza uyğun namespace
{
    /// <summary>
    /// Configures the database mapping for the Testimonial entity.
    /// </summary>
    public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
    {
        public void Configure(EntityTypeBuilder<Testimonial> builder)
        {
            // 1. Cədvəlin adını təyin edirik.
            builder.ToTable("Testimonials");

            // 2. Əsas açarı (Primary Key) təyin edirik.
            // Bu, EntityBase-dən gəlsə də, burada bir daha dəqiqləşdirmək yaxşı təcrübədir.
            builder.HasKey(t => t.Id);

            // 3. Hər bir property üçün qaydaları təyin edirik.
            builder.Property(t => t.ClientName)
                .IsRequired() // Bu sahənin boş olmamalı olduğunu bildirir.
                .HasMaxLength(100); // Verilənlər bazasındakı maksimum uzunluğu təyin edir.

            builder.Property(t => t.Profession)
                .IsRequired(false) // Bu sahə boş ola bilər.
                .HasMaxLength(100);

            builder.Property(t => t.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(500); // URL-lər uzun ola bilər.

            builder.Property(t => t.Comment)
                .IsRequired()
                .HasMaxLength(1000); // Rəy mətni üçün daha çox yer ayırırıq.

            // Rating üçün xüsusi bir qaydaya ehtiyac yoxdur, çünki int tipidir.
            // Amma məsələn, default dəyər təyin edə bilərik:
            builder.Property(t => t.Rating)
                .HasDefaultValue(5);

            // IsActive üçün də default dəyər təyin edirik.
            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            // Testimonial entity-sinin başqa cədvəllərlə birbaşa əlaqəsi olmadığı üçün
            // burada HasOne/HasMany/WithOne kimi əlaqə konfiqurasiyaları yoxdur.
        }
    }
}