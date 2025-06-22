using MediatR;
using Product.Application.Dtos.ProductTags;
using System.Collections.Generic;

namespace Product.Application.Features.ProductTags.Queries
{
    public class GetAllTagsWithProductCountQuery : IRequest<IReadOnlyList<ProductTagWithProductCountDto>> { }
}