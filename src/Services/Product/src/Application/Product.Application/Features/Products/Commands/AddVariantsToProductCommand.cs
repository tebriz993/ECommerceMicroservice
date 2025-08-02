using MediatR;
using Product.Application.Dtos.Product;
using System;
using System.Collections.Generic;

namespace Product.Application.Features.Products.Commands
{
    public class AddVariantsToProductCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public List<CreateProductVariantDto> Variants { get; set; }
    }
}