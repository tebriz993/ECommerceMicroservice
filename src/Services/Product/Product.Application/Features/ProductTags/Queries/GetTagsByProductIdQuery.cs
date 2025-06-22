using MediatR;
using Product.Application.Dtos.ProductTags;
using System;
using System.Collections.Generic;

namespace Product.Application.Features.ProductTags.Queries
{
    public class GetTagsByProductIdQuery : IRequest<IReadOnlyList<ProductTagDto>>
    {
        public Guid ProductId { get; set; }
    }
}