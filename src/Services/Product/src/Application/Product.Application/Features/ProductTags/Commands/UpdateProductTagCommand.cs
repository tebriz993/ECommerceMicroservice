using MediatR;
using Product.Application.Dtos.ProductTags;
using System;

namespace Product.Application.Features.ProductTags.Commands
{
    public class UpdateProductTagCommand : IRequest
    {
        public Guid Id { get; set; }
        public UpdateProductTagDto UpdateTagDto { get; set; }
    }
}