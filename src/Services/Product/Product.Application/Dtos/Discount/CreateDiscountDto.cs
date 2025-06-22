using Product.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Application.Dtos.Discount
{
    public class CreateDiscountDto : DiscountBaseDto
    {
        /// <summary>
        /// A list of product IDs to apply this discount to.
        /// </summary>
        public List<Guid> ApplicableProductIds { get; set; } = new List<Guid>();

        /// <summary>
        /// A list of category IDs to apply this discount to.
        /// </summary>
        public List<Guid> ApplicableCategoryIds { get; set; } = new List<Guid>();
    }
}