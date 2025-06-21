using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Implements the IDiscountRepository for data access operations related to Discounts.
    /// </summary>
    public class DiscountRepository : RepositoryBase<Discount>, IDiscountRepository
    {
        public DiscountRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Discount>> GetActiveDiscountsAsync(bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Discounts.AsNoTracking() : DbContext.Discounts;
            var now = DateTime.UtcNow;

            return await query
                .Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Discount>> GetApplicableDiscountsForProductAsync(Guid productId, bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Discounts.AsNoTracking() : DbContext.Discounts;
            var now = DateTime.UtcNow;

            // Bu sorğu bir məhsula birbaşa tətbiq olunan VƏ YA
            // həmin məhsulun kateqoriyasına tətbiq olunan bütün aktiv endirimləri gətirir.
            return await query
                .Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now)
                .Where(d =>
                    d.ApplicableProducts.Any(p => p.Id == productId) ||
                    d.ApplicableCategories.Any(c => c.Products.Any(p => p.Id == productId))
                )
                .Distinct() // Eyni endirimin həm məhsula, həm kateqoriyaya tətbiqi halında təkrarlanmaması üçün
                .ToListAsync();
        }
    }
}