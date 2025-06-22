using MediatR;
using Product.Application.Dtos.Product;
using System;

namespace Product.Application.Features.Products.Commands
{
    public class UpdateProductImageCommand : IRequest
    {
        public Guid ImageId { get; set; }
        public UpdateProductImageDto UpdateDto { get; set; }
    }
}