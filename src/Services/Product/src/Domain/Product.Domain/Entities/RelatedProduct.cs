using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class RelatedProduct : EntityBase
    {
        public Guid ProductId { get; set; }
        public Products Product { get; set; }

        public Guid RelatedProductId { get; set; }
        public Products RelatedToProduct { get; set; }

        public int DisplayOrder { get; set; } = 0;
    }
}
