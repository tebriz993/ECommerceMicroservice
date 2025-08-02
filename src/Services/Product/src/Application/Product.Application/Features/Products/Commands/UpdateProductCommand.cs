using MediatR;
using Product.Application.Dtos.Product;
using System;

namespace Product.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public UpdateProductDto UpdateProductDto { get; set; }
    }
}