using Product.Application.Features.Discount.Queries;
using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IDiscountRepository : IRepositoryBase<Discount>
    {
        Task<(IEnumerable<Discount> Discounts, int TotalCount)> GetDiscountsByPageAsync(GetDiscountsByPageQuery queryParams);
        Task<IReadOnlyList<Discount>> GetActiveDiscountsAsync(bool trackChanges = false);
        Task<Discount?> GetDiscountWithApplicablesByIdAsync(Guid id, bool trackChanges = false);
        Task<IReadOnlyList<Discount>> GetApplicableDiscountsForProductAsync(Guid productId, bool trackChanges = false);
    }
}