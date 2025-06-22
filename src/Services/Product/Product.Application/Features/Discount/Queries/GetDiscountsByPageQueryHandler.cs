using AutoMapper;
using MediatR;
using Product.Application.Dtos.Discount;
using Product.Application.Interfaces;
using Product.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Queries
{
    public class GetDiscountsByPageQueryHandler : IRequestHandler<GetDiscountsByPageQuery, PagedResponse<IReadOnlyList<DiscountDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDiscountsByPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IReadOnlyList<DiscountDto>>> Handle(GetDiscountsByPageQuery request, CancellationToken cancellationToken)
        {
            // Bu metod üçün IDiscountRepository-də yeni bir metoda ehtiyac var.
            // Məsələn: Task<(IEnumerable<Discount> Discounts, int TotalCount)> GetDiscountsByPageAsync(GetDiscountsByPageQuery query);
            var (discounts, totalCount) = await _unitOfWork.DiscountRepository.GetDiscountsByPageAsync(request);

            var discountDtos = _mapper.Map<IReadOnlyList<DiscountDto>>(discounts);

            return new PagedResponse<IReadOnlyList<DiscountDto>>(discountDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}