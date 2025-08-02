using AutoMapper;
using MediatR;
using Product.Application.Dtos.Discount;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Queries
{
    public class GetActiveDiscountsQueryHandler : IRequestHandler<GetActiveDiscountsQuery, IReadOnlyList<DiscountDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetActiveDiscountsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DiscountDto>> Handle(GetActiveDiscountsQuery request, CancellationToken cancellationToken)
        {
            // Call the specific method in the repository.
            var activeDiscounts = await _unitOfWork.DiscountRepository.GetActiveDiscountsAsync();

            // Map the result to DTOs.
            return _mapper.Map<IReadOnlyList<DiscountDto>>(activeDiscounts);
        }
    }
}