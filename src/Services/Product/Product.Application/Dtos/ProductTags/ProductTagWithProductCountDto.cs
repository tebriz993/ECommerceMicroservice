using Product.Application.Dtos.Base;
using System;

namespace Product.Application.Dtos.ProductTags
{
    public class ProductTagWithProductCountDto : ProductTagBaseDto
    {
        public Guid Id { get; set; }
        public int ProductCount { get; set; }
    }
}