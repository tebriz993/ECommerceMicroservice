using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Discount.Queries;
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
    public class DiscountRepository : RepositoryBase<Discount>, IDiscountRepository
    {
        public DiscountRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<(IEnumerable<Discount> Discounts, int TotalCount)> GetDiscountsByPageAsync(GetDiscountsByPageQuery queryParams)
        {
            // 1. Əsas sorğunu başladıq.
            IQueryable<Discount> query = DbContext.Discounts.AsNoTracking();

            // 2. Filterləmə (Filtering)
            if (queryParams.IsActive.HasValue)
            {
                var now = DateTime.UtcNow;
                if (queryParams.IsActive.Value)
                {
                    // Yalnız aktiv olanları gətir
                    query = query.Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now);
                }
                else
                {
                    // Yalnız passiv olanları gətir
                    query = query.Where(d => !d.IsActive || d.EndDate < now);
                }
            }

            // 3. Ümumi sayı hesablama (Səhifələmədən ƏVVƏL)
            var totalCount = await query.CountAsync();

            // 4. Səhifələmə (Pagination)
            var pagedQuery = query
                .OrderByDescending(d => d.CreatedAt) // Default olaraq yeni yaradılanlara görə sıralayırıq
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize);

            var discounts = await pagedQuery.ToListAsync();

            // 5. Nəticəni Tuple olaraq qaytarırıq.
            return (discounts, totalCount);
        }
        public async Task<IReadOnlyList<Discount>> GetActiveDiscountsAsync(bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Discounts.AsNoTracking() : DbContext.Discounts;
            var now = DateTime.UtcNow;

            return await query
                .Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now)
                .ToListAsync();
        }

        public async Task<Discount?> GetDiscountWithApplicablesByIdAsync(Guid id, bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Discounts.AsNoTracking() : DbContext.Discounts;

            return await query
                .Include(d => d.ApplicableProducts)
                .Include(d => d.ApplicableCategories)
                .FirstOrDefaultAsync(d => d.Id == id);
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