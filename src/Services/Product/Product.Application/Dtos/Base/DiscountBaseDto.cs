using Product.Domain.Enums;
using System;

namespace Product.Application.Dtos.Base
{
    public abstract class DiscountBaseDto
    {
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}