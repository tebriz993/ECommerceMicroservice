using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class ProductVariant : EntityBase
    {
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public string Name { get; set; } // e.g., "Color", "Size"
        public string Value { get; set; } // e.g., "Red", "XL"
        public decimal PriceAdjustment { get; set; } = 0.00m;
        public int Quantity { get; set; } = 0;
        public string SKU { get; set; }
    }
}
