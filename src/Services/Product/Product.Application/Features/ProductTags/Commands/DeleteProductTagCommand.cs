using MediatR;
using System;

namespace Product.Application.Features.ProductTags.Commands
{
    public class DeleteProductTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}