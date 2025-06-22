using MediatR;
using System;

namespace Product.Application.Features.Discount.Commands
{
    public class DeleteDiscountCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}