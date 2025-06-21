// Fayl: Product.Application/Interfaces/IProductRepository.cs
using Product.Application.Features.Products.Queries;
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;

namespace Product.Application.Interfaces;

public interface IProductRepository : IRepositoryBase<Products>
{
    // DÜZƏLİŞ: Artıq yalnız özünə aid metodlar var.
    // Bu metod, bir məhsulu əlaqəli bütün dataları ilə birlikdə gətirir.
    Task<Products?> GetProductWithDetailsByIdAsync(Guid id, bool trackChanges = false);

    // Nümunə olaraq başqa spesifik metodlar da əlavə edə bilərik:
    Task<IReadOnlyList<Products>> GetFeaturedProductsAsync(int count, bool trackChanges = false);
    Task<(IEnumerable<Products> Products, int TotalCount)> GetProductsByPageAsync(GetProductsByPageQuery queryParams);
}