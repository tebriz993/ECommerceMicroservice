using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Commands
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDiscountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = _mapper.Map<Domain.Entities.Discount>(request.DiscountDto);

            // Tətbiq olunacaq məhsulları ID-lərinə görə tapıb əlavə edirik.
            if (request.DiscountDto.ApplicableProductIds != null && request.DiscountDto.ApplicableProductIds.Any())
            {
                // DÜZƏLİŞ: Metodun adı FindByConditionAsync olmalıdır.
                var products = await _unitOfWork.ProductRepository
                    .FindByConditionAsync(p => request.DiscountDto.ApplicableProductIds.Contains(p.Id));
                discount.ApplicableProducts = products.ToList();
            }

            // Tətbiq olunacaq kateqoriyaları ID-lərinə görə tapıb əlavə edirik.
            if (request.DiscountDto.ApplicableCategoryIds != null && request.DiscountDto.ApplicableCategoryIds.Any())
            {
                var categories = await _unitOfWork.CategoryRepository
                    .FindByConditionAsync(c => request.DiscountDto.ApplicableCategoryIds.Contains(c.Id));
                discount.ApplicableCategories = categories.ToList();
            }

            await _unitOfWork.DiscountRepository.AddAsync(discount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return discount.Id;
        }
    }
}