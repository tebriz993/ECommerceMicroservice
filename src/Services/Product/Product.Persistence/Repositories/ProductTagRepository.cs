using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Application.Interfaces;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;

namespace Product.Persistence.Repositories;

public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagRepository
{
    public ProductTagRepository(ProductDbContext dbContext) : base(dbContext) { }

    public async Task<ProductTag?> GetByNameAsync(string name)
    {
        return await DbContext.ProductTags
            .FirstOrDefaultAsync(pt => pt.Name.ToLower() == name.ToLower());
    }

    public async Task<IReadOnlyList<ProductTag>> GetTagsByProductIdAsync(Guid productId)
    {
        // Many-to-Many əlaqəsi olduğu üçün bu sorğu bir az fərqlidir.
        return await DbContext.ProductTags
            .Where(pt => pt.Products.Any(p => p.Id == productId))
            .ToListAsync();
    }
}