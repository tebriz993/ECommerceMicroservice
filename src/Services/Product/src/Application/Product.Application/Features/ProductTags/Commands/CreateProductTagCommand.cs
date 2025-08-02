using MediatR;
using Product.Application.Dtos.ProductTags;
using System;

namespace Product.Application.Features.ProductTags.Commands
{
    public class CreateProductTagCommand : IRequest<Guid>
    {
        public CreateProductTagDto CreateTagDto { get; set; }
    }
}