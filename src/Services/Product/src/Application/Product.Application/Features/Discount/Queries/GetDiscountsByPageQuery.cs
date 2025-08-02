using MediatR;
using Product.Application.Dtos.Discount;
using Product.Application.Wrappers;
using System.Collections.Generic;

namespace Product.Application.Features.Discount.Queries
{
    public class GetDiscountsByPageQuery : IRequest<PagedResponse<IReadOnlyList<DiscountDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? IsActive { get; set; }
    }
}