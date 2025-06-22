using MediatR;
using Product.Application.Dtos.Product; // CreateProductImageDto üçün
using System;
using System.Collections.Generic;

namespace Product.Application.Features.Products.Commands
{
    public class AddImagesToProductCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public List<CreateProductImageDto> Images { get; set; }
    }
}