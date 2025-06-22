using Product.Application.Dtos.Base;
using System;

namespace Product.Application.Dtos.ProductTags
{
    public class ProductTagDto : ProductTagBaseDto
    {
        public Guid Id { get; set; }
    }
}