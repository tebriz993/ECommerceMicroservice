using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence;
using Product.Persistence.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Persistence.Repositories
{
    /// <summary>
    /// Implements the ITestimonialRepository for data access operations related to Testimonials.
    /// </summary>
    public class TestimonialRepository : RepositoryBase<Testimonial>, ITestimonialRepository
    {
        public TestimonialRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Testimonial>> GetFeaturedTestimonialsAsync(int count, bool trackChanges = false)
        {
            var query = !trackChanges ? DbContext.Testimonials.AsNoTracking() : DbContext.Testimonials;

            return await query
                .Where(t => t.IsActive)
                .OrderByDescending(t => t.CreatedAt) // Ən yeniləri göstərmək üçün
                .Take(count)
                .ToListAsync();
        }
    }
}