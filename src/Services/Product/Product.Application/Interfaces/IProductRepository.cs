using Product.Application.Features.Products.Queries;
using Product.Application.Interfaces.Base; // IRepositoryBase üçün
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    // Artıq IRepositoryBase<Products>-dən miras alır.
    // Bu o deməkdir ki, GetAllAsync, GetByIdAsync, AddAsync, Update, Delete metodları avtomatik olaraq bu interfeysə aiddir.
    public interface IProductRepository : IRepositoryBase<Products>
    {
        // --- Yalnız Spesifik Product Queries ---
        Task<Products?> GetProductWithDetailsByIdAsync(Guid id, bool trackChanges = false);
        Task<IReadOnlyList<Products>> GetFeaturedProductsAsync(int count, bool trackChanges = false);
        Task<(IEnumerable<Products> Products, int TotalCount)> GetProductsByPageAsync(GetProductsByPageQuery queryParams);

        // --- ProductImage Metodları ---
        Task<ProductImage?> GetImageByIdAsync(Guid imageId);
        Task<IReadOnlyList<ProductImage>> GetImagesByProductIdAsync(Guid productId);
        Task AddImageAsync(ProductImage image);
        void DeleteImage(ProductImage image);
        void UpdateImage(ProductImage image); // Əlavə edirik

        // --- ProductVariant Metodları ---
        Task<ProductVariant?> GetVariantByIdAsync(Guid variantId);
        Task<IReadOnlyList<ProductVariant>> GetVariantsByProductIdAsync(Guid productId);
        Task AddVariantAsync(ProductVariant variant);
        void DeleteVariant(ProductVariant variant);
        void UpdateVariant(ProductVariant variant); // Əlavə edirik
    }
}