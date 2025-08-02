using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class ProductTag : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
