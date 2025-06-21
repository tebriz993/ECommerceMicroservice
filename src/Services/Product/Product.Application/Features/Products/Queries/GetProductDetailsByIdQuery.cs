using MediatR;
using Product.Application.Dtos.Product;
using System;

namespace Product.Application.Features.Products.Queries
{
    /// <summary>
    /// Represents a query to get the full details of a single product.
    /// </summary>
    public class GetProductDetailsByIdQuery : IRequest<ProductDetailsDto>
    {
        public Guid Id { get; set; }
    }
}