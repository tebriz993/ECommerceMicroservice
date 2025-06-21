using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Application.Interfaces;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;

namespace Product.Persistence.Repositories;

public class ProductVariantRepository : RepositoryBase<ProductVariant>, IProductVariantRepository
{
    public ProductVariantRepository(ProductDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyList<ProductVariant>> GetVariantsByProductIdAsync(Guid productId)
    {
        return await FindByConditionAsync(pv => pv.ProductId == productId);
    }

    public async Task UpdateStockAsync(Guid variantId, int quantityChange)
    {
        var variant = await GetByIdAsync(variantId);
        if (variant != null)
        {
            // Burada biznes məntiqi ola bilər (məs. stok mənfiyə düşə bilməz)
            // Amma bu, Application qatının məsuliyyətidir. Repozitori sadəcə datanı dəyişir.
            variant.Quantity += quantityChange;
            Update(variant); // Base class-dakı Update metodunu çağırırıq.
        }
    }
}