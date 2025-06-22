using Product.Application.Features.Testimonial.Queries;
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface ITestimonialRepository : IRepositoryBase<Testimonial>
    {
        Task<IReadOnlyList<Testimonial>> GetFeaturedTestimonialsAsync(int count, bool trackChanges = false);
        Task<(IEnumerable<Testimonial> Testimonials, int TotalCount)> GetTestimonialsByPageAsync(GetTestimonialsByPageQuery queryParams);
    }
}