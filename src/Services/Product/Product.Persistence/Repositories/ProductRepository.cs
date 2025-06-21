using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Products.Queries;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;

namespace Product.Persistence.Repositories;

public class ProductRepository : RepositoryBase<Products>, IProductRepository
{
    public ProductRepository(ProductDbContext dbContext) : base(dbContext) { }

    // Bu metod IProductRepository interfeysində təyin olunub.
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

    // Bu metod da IProductRepository interfeysində təyin olunub.
    public async Task<IReadOnlyList<Products>> GetFeaturedProductsAsync(int count, bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.Products.AsNoTracking() : DbContext.Products;

        // "IsFeatured" adlı bir property-nin Product entity-sində olduğunu fərz edirik.
        return await query
            .Where(p => p.IsFeatured)
            .Include(p => p.Images) // Önsəhifədə şəkil göstərmək üçün
            .Take(count)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Products> Products, int TotalCount)> GetProductsByPageAsync(GetProductsByPageQuery queryParams)
    {
        // 1. Əsas sorğunu (query) başladıq. Hələ bazaya getmirik.
        IQueryable<Products> query = DbContext.Products.AsNoTracking();

        // 2. Filterləmə (Filtering)
        // Kateqoriyaya görə
        if (queryParams.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == queryParams.CategoryId.Value);
        }

        // Axtarış sözünə görə
        if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
        {
            var searchTermLower = queryParams.SearchTerm.ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(searchTermLower) || p.Description.ToLower().Contains(searchTermLower));
        }

        // Qiymət aralığına görə
        if (queryParams.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= queryParams.MinPrice.Value);
        }
        if (queryParams.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= queryParams.MaxPrice.Value);
        }

        // Teqlərə görə (əgər göndərilibsə)
        if (queryParams.TagIds != null && queryParams.TagIds.Any())
        {
            query = query.Where(p => p.Tags.Any(t => queryParams.TagIds.Contains(t.Id)));
        }

        // 3. Ümumi sayı hesablama (Sıralama və səhifələmədən ƏVVƏL)
        var totalCount = await query.CountAsync();

        // 4. Sıralama (Sorting)
        query = queryParams.SortBy?.ToLower() switch
        {
            "price" => queryParams.IsAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            "name" => queryParams.IsAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            // "rating" və ya digər sıralama növləri əlavə edilə bilər
            _ => query.OrderByDescending(p => p.CreatedAt) // Default olaraq yeni əlavə olunanlar
        };

        // 5. Səhifələmə (Pagination)
        var pagedQuery = query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Include(p => p.Images); // Siyahı səhifəsi üçün yalnız şəkilləri yükləmək kifayətdir.

        var products = await pagedQuery.ToListAsync();

        // 6. Nəticəni Tuple olaraq qaytarırıq.
        return (products, totalCount);
    }

    // DİQQƏT: GetImagesByProductIdAsync və GetMainImageByProductIdAsync metodları
    // buradan tamamilə silindi, çünki onlar IProductImageRepository-ə aiddir.
}