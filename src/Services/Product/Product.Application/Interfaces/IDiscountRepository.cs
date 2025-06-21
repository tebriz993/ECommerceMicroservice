using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for the discount repository.
    /// </summary>
    public interface IDiscountRepository : IRepositoryBase<Discount>
    {
        /// <summary>
        /// Gets all currently active discounts.
        /// </summary>
        Task<IReadOnlyList<Discount>> GetActiveDiscountsAsync(bool trackChanges = false);

        /// <summary>
        /// Gets all discounts applicable to a specific product,
        /// including discounts on its category.
        /// </summary>
        Task<IReadOnlyList<Discount>> GetApplicableDiscountsForProductAsync(Guid productId, bool trackChanges = false);
    }
}