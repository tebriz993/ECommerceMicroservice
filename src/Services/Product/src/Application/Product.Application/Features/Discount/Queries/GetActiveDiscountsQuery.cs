using MediatR;
using Product.Application.Dtos.Discount;
using System.Collections.Generic;

namespace Product.Application.Features.Discount.Queries
{
    public class GetActiveDiscountsQuery : IRequest<IReadOnlyList<DiscountDto>> { }
}