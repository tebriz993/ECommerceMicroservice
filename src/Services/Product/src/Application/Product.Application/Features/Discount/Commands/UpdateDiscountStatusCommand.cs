using MediatR;
using System;

namespace Product.Application.Features.Discount.Commands
{
    public class UpdateDiscountStatusCommand : IRequest
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}