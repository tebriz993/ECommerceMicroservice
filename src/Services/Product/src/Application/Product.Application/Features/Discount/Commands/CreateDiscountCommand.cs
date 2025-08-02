using MediatR;
using Product.Application.Dtos.Discount;
using System;

namespace Product.Application.Features.Discount.Commands
{
    public class CreateDiscountCommand : IRequest<Guid>
    {
        public CreateDiscountDto DiscountDto { get; set; }
    }
}