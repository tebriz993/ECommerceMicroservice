using Product.Application.Features.Categories.Queries;
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    // Bütün kateqoriyaları məhsulları ilə gətirir.
    Task<IEnumerable<Category>> GetCategoriesWithProductsAsync(bool trackChanges = false);

    // YENİ ƏLAVƏ EDİLDİ: Tək bir kateqoriyanı ID ilə və məhsulları ilə birlikdə gətirir.
    Task<Category?> GetCategoryWithProductsByIdAsync(Guid id, bool trackChanges = false);

    Task<IReadOnlyList<Category>> GetChildCategoriesAsync(Guid? parentId, bool trackChanges = false);
    Task<IReadOnlyList<Category>> SearchCategoriesAsync(string searchTerm, bool? isActive, int? maxItems);
    Task<(IEnumerable<Category> Categories, int TotalCount)> GetCategoriesByPageAsync(GetCategoriesByPageQuery query);

}