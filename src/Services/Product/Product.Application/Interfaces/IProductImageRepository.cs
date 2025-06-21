// Product.Application/Interfaces/IProductImageRepository.cs
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IProductImageRepository:IRepositoryBase<ProductImage>
    {
        Task<IReadOnlyList<ProductImage>> GetImagesByProductIdAsync(Guid productId);
        Task<ProductImage?> GetMainImageByProductIdAsync(Guid productId);
    }
}