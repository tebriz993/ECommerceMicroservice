using MediatR;
using Product.Application.Dtos.Discount; // DiscountDetailsDto üçün
using System;

namespace Product.Application.Features.Discount.Queries
{
    public class GetDiscountDetailsByIdQuery : IRequest<DiscountDetailsDto>
    {
        public Guid Id { get; set; }
    }
}