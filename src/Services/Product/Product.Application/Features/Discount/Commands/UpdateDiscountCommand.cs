using MediatR;
using Product.Application.Dtos.Discount;
using System;

namespace Product.Application.Features.Discount.Commands
{
    public class UpdateDiscountCommand : IRequest
    {
        public Guid Id { get; set; }
        public UpdateDiscountDto DiscountDto { get; set; }
    }
}