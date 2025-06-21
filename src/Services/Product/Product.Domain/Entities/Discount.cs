using Product.Domain.Common;
using Product.Domain.Enums; // DiscountType üçün yeni bir Enum yaradacağıq
using System;
using System.Collections.Generic;

namespace Product.Domain.Entities
{
    /// <summary>
    /// Represents a discount that can be applied to products or categories.
    /// </summary>
    public class Discount : EntityBase
    {
        /// <summary>
        /// The name of the discount (e.g., "Summer Sale", "Black Friday").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the discount (e.g., percentage or a fixed amount).
        /// </summary>
        public DiscountType DiscountType { get; set; }

        /// <summary>
        /// The value of the discount. If percentage, this is 1-100. If fixed, it's a monetary value.
        /// </summary>
        public decimal DiscountValue { get; set; }

        /// <summary>
        /// The start date of the discount period.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the discount period.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Controls whether the discount is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        // --- Əlaqələr (Relationships) ---

        /// <summary>
        /// The list of specific products this discount applies to.
        /// If this is empty, the discount might apply to categories.
        /// </summary>
        public ICollection<Products> ApplicableProducts { get; set; } = new List<Products>();

        /// <summary>
        /// The list of categories this discount applies to.
        /// </summary>
        public ICollection<Category> ApplicableCategories { get; set; } = new List<Category>();
    }
}