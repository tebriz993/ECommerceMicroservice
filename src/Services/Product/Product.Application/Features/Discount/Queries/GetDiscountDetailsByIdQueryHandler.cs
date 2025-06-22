using AutoMapper;
using MediatR;
using Product.Application.Dtos.Discount;
using Product.Application.Exceptions; // NotFoundException üçün
using Product.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Queries
{
    public class GetDiscountDetailsByIdQueryHandler : IRequestHandler<GetDiscountDetailsByIdQuery, DiscountDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDiscountDetailsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DiscountDetailsDto> Handle(GetDiscountDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Repozitoridəki xüsusi metodu çağırırıq ki, əlaqəli məhsul və kateqoriyalar da gəlsin.
            var discount = await _unitOfWork.DiscountRepository
                .GetDiscountWithApplicablesByIdAsync(request.Id, trackChanges: false);

            // 2. Əgər endirim tapılmazsa, xüsusi bir exception atırıq.
            if (discount is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Discount), request.Id);
            }

            // 3. AutoMapper vasitəsilə Entity-ni detallı DTO-ya çeviririk.
            // Bu, DiscountProfile-da təyin etdiyimiz map-ə əsasən işləyəcək.
            return _mapper.Map<DiscountDetailsDto>(discount);
        }
    }
}