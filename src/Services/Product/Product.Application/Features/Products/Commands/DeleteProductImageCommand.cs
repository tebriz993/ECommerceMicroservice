using MediatR;
using System;

namespace Product.Application.Features.Products.Commands
{
    public class DeleteProductImageCommand : IRequest
    {
        public Guid ImageId { get; set; }
    }
}