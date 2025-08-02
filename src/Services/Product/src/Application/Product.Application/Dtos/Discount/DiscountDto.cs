using Product.Application.Dtos.Base;
using System;

namespace Product.Application.Dtos.Discount
{
    public class DiscountDto : DiscountBaseDto
    {
        public Guid Id { get; set; }
    }
}