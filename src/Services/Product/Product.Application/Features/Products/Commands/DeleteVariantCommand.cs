using MediatR;
using System;

namespace Product.Application.Features.Products.Commands
{
    public class DeleteVariantCommand : IRequest
    {
        public Guid VariantId { get; set; }
    }
}