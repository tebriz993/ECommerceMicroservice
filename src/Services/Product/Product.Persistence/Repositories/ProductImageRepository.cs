// Fayl: Product.Persistence/Repositories/ProductImageRepository.cs
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;

namespace Product.Persistence.Repositories;

public class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(ProductDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyList<ProductImage>> GetImagesByProductIdAsync(Guid productId, bool trackChanges = false)
    {
        return await FindByConditionAsync(pi => pi.ProductId == productId, trackChanges);
    }

    public Task<IReadOnlyList<ProductImage>> GetImagesByProductIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductImage?> GetMainImageByProductIdAsync(Guid productId, bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.ProductImages.AsNoTracking() : DbContext.ProductImages;
        return await query.FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IsMainImage);
    }

    public Task<ProductImage?> GetMainImageByProductIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }
}