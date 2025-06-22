using MediatR;
using System;

namespace Product.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}