using MediatR;
using Product.Application.Dtos.Product;
using System;

namespace Product.Application.Features.Products.Commands
{
    /// <summary>
    /// Represents the command to create a new product.
    /// The handler will return the ID of the newly created product.
    /// </summary>
    public class CreateProductCommand : IRequest<Guid>
    {
        public CreateProductDto CreateProductDto { get; set; }
    }
}