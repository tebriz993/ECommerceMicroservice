// Product.Application/Interfaces/IProductVariantRepository.cs
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IProductVariantRepository:IRepositoryBase<ProductVariant>
    {
        Task<IReadOnlyList<ProductVariant>> GetVariantsByProductIdAsync(Guid productId);
        Task UpdateStockAsync(Guid variantId, int quantityChange);
    }
}