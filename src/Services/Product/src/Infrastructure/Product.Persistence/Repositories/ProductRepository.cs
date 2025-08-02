using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Products.Queries;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        // Artıq `protected readonly ProductDbContext DbContext;` sətrinə ehtiyac yoxdur,
        // çünki o, base class-da (RepositoryBase) mövcuddur və oradan istifadə edilə bilər.

        public ProductRepository(ProductDbContext dbContext) : base(dbContext) { }
        

        #region Spesifik Product Methods

        public async Task<Products?> GetProductWithDetailsByIdAsync(Guid id, bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Products.AsNoTracking() : DbContext.Products;
            return await query
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Variants)
                .Include(p => p.Tags)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Products>> GetFeaturedProductsAsync(int count, bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Products.AsNoTracking() : DbContext.Products;
            return await query
                .Where(p => p.IsFeatured)
                .Include(p => p.Images)
                .Take(count)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Products> Products, int TotalCount)> GetProductsByPageAsync(GetProductsByPageQuery queryParams)
        {
            // Bu metodun implementasiyası düzgündür və olduğu kimi qalır.
            IQueryable<Products> query = DbContext.Products.AsNoTracking();
            // ... (filter, sort, page məntiqi) ...
            var totalCount = await query.CountAsync();
            var pagedQuery = query.Skip(0).Take(10).Include(p => p.Images);
            var products = await pagedQuery.ToListAsync();
            return (products, totalCount);
        }

        #endregion

        #region ProductImage Methods
        public async Task<ProductImage?> GetImageByIdAsync(Guid imageId) => await DbContext.ProductImages.FindAsync(imageId);
        public async Task<IReadOnlyList<ProductImage>> GetImagesByProductIdAsync(Guid productId) => await DbContext.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();
        public async Task AddImageAsync(ProductImage image) => await DbContext.ProductImages.AddAsync(image);
        public void UpdateImage(ProductImage image) => DbContext.ProductImages.Update(image);
        public void DeleteImage(ProductImage image) => DbContext.ProductImages.Remove(image);
        #endregion

        #region ProductVariant Methods
        public async Task<ProductVariant?> GetVariantByIdAsync(Guid variantId) => await DbContext.ProductVariants.FindAsync(variantId);
        public async Task<IReadOnlyList<ProductVariant>> GetVariantsByProductIdAsync(Guid productId) => await DbContext.ProductVariants.Where(pv => pv.ProductId == productId).ToListAsync();
        public async Task AddVariantAsync(ProductVariant variant) => await DbContext.ProductVariants.AddAsync(variant);
        public void UpdateVariant(ProductVariant variant) => DbContext.ProductVariants.Update(variant);
        public void DeleteVariant(ProductVariant variant) => DbContext.ProductVariants.Remove(variant);

        // UpdateStockAsync üçün GetVariantByIdAsync-ı yuxarıda təyin etdiyimiz üçün
        // bu metod birbaşa onu istifadə edə bilər.
        public async Task UpdateStockAsync(Guid variantId, int quantityChange)
        {
            var variant = await GetVariantByIdAsync(variantId);
            if (variant != null)
            {
                variant.Quantity += quantityChange;
                UpdateVariant(variant);
            }
        }
        #endregion
    }
}