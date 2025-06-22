using MediatR;
using System;

namespace Product.Application.Features.Products.Commands
{
    public class UpdateVariantStockCommand : IRequest
    {
        public Guid VariantId { get; set; }
        public int QuantityChange { get; set; }
    }
}