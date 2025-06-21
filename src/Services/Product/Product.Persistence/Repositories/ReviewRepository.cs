using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Persistence.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Review>> GetReviewsByProductIdAsync(Guid productId, bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Reviews.AsNoTracking() : DbContext.Reviews;
            return await query
                 .Where(r => r.ProductId == productId)
                 .OrderByDescending(r => r.CreatedAt)
                 .ToListAsync();
        }

    }
}
