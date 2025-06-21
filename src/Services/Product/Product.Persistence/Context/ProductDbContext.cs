using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Configurations;
using System.Reflection;

namespace Product.Infrastructure.Persistence.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<RelatedProduct> RelatedProducts { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TƏKMİLLƏŞDİRMƏ: Bütün konfiqurasiyaları bu assembly-dən avtomatik tapır.
            // Bu, yeni bir konfiqurasiya əlavə etdikdə bu faylı dəyişməyi unutmamağa kömək edir.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}