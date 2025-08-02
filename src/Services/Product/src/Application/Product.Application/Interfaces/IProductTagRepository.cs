// Product.Application/Interfaces/IProductTagRepository.cs
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IProductTagRepository:IRepositoryBase<ProductTag>
    {
        Task<ProductTag> GetByNameAsync(string name);
        Task<IReadOnlyList<ProductTag>> GetTagsByProductIdAsync(Guid productId);
        Task<IReadOnlyList<ProductTag>> GetAllWithProductCountAsync(bool trackChanges = false);
    }
}