using Product.Application.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Product.Application.Dtos.Discount
{
    public class UpdateDiscountDto : DiscountBaseDto
    {
        /// <summary>
        /// The complete new list of product IDs for this discount.
        /// The handler will sync the relationships based on this list.
        /// </summary>
        public List<Guid> ApplicableProductIds { get; set; } = new List<Guid>();

        /// <summary>
        /// The complete new list of category IDs for this discount.
        /// </summary>
        public List<Guid> ApplicableCategoryIds { get; set; } = new List<Guid>();
    }
}