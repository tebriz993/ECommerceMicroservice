using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Categories.Queries;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Persistence.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(ProductDbContext dbContext) : base(dbContext) { }

    public async Task<(IEnumerable<Category> Categories, int TotalCount)> GetCategoriesByPageAsync(GetCategoriesByPageQuery queryParams)
    {
        var query = DbContext.Categories.AsNoTracking();

        // Axtarış
        if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
        {
            query = query.Where(c => c.Name.ToLower().Contains(queryParams.SearchTerm.ToLower()));
        }

        // Ümumi sayı hesablama (sıralama və səhifələmədən əvvəl)
        var totalCount = await query.CountAsync();

        // Sıralama
        if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
        {
            // Gələcəkdə daha dinamik bir sıralama mexanizmi qurula bilər.
            // Hələlik sadəcə "Name"-ə görə edək.
            query = queryParams.IsAscending
                ? query.OrderBy(c => c.Name)
                : query.OrderByDescending(c => c.Name);
        }

        // Səhifələmə
        var pagedQuery = query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize);

        var categories = await pagedQuery.ToListAsync();

        return (categories, totalCount);
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync(bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.Categories.AsNoTracking() : DbContext.Categories;
        return await query.Include(c => c.Products).ToListAsync();
    }

    // YENİ ƏLAVƏ EDİLDİ: Yeni metodun implementasiyası.
    public async Task<Category?> GetCategoryWithProductsByIdAsync(Guid id, bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.Categories.AsNoTracking() : DbContext.Categories;
        return await query
            .Include(c => c.Products) // Məhsulları da sorğuya daxil edirik.
            .FirstOrDefaultAsync(c => c.Id == id); // Verilən ID-yə uyğun ilk obyekti tapırıq.
    }

    public async Task<IReadOnlyList<Category>> GetChildCategoriesAsync(Guid? parentId, bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.Categories.AsNoTracking() : DbContext.Categories;

        // ParentCategoryId-si verilən dəyərə bərabər olan bütün kateqoriyaları tapır.
        // Əgər parentId null-dırsa, bu, ana (root) kateqoriyaları gətirəcək.
        return await query
            .Where(c => c.ParentCategoryId == parentId)
            .OrderBy(c => c.DisplayOrder) // Gələcəkdə sıralama üçün
            .ToListAsync();
    }
    public async Task<IReadOnlyList<Category>> SearchCategoriesAsync(string searchTerm, bool? isActive, int? maxItems)
    {
        // Axtarış üçün həmişə `AsNoTracking()` istifadə etmək daha yaxşıdır, çünki bu datanı dəyişmək niyyətimiz yoxdur.
        var query = DbContext.Categories.AsNoTracking();

        // 1. Axtarış sözünə görə filterləmə (əgər göndərilibsə)
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        // 2. Aktivlik statusuna görə filterləmə (əgər göndərilibsə)
        if (isActive.HasValue)
        {
            query = query.Where(c => c.IsActive == isActive.Value);
        }

        // 3. Nəticə sayını məhdudlaşdırma (əgər göndərilibsə)
        if (maxItems.HasValue)
        {
            query = query.Take(maxItems.Value);
        }

        return await query.ToListAsync();
    }
}