using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Commands
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDiscountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            // DÜZƏLİŞ: Artıq mövcud olan metodu çağırırıq.
            var discountToUpdate = await _unitOfWork.DiscountRepository
                .GetDiscountWithApplicablesByIdAsync(request.Id, trackChanges: true);

            if (discountToUpdate is null)
                throw new KeyNotFoundException($"Discount with ID '{request.Id}' not found.");

            // Əsas məlumatları yeniləyirik.
            _mapper.Map(request.DiscountDto, discountToUpdate);

            // Məhsul və Kateqoriya əlaqələrini yeniləyirik (sync).
            // Köhnə əlaqələri təmizləyib yenilərini əlavə etmək yerinə,
            // EF Core-un "collection tracking"-i ilə işləmək daha effektivdir.
            var products = await _unitOfWork.ProductRepository
                .FindByConditionAsync(p => request.DiscountDto.ApplicableProductIds.Contains(p.Id));
            discountToUpdate.ApplicableProducts = products.ToList(); // EF Core fərqi tapıb bazanı yeniləyəcək.

            var categories = await _unitOfWork.CategoryRepository
                .FindByConditionAsync(c => request.DiscountDto.ApplicableCategoryIds.Contains(c.Id));
            discountToUpdate.ApplicableCategories = categories.ToList();

            // Artıq EF Core obyektin dəyişdiyini bilir, Update çağırmaq artıqdır,
            // amma çağırmaq da zərər vermir.
            // _unitOfWork.DiscountRepository.Update(discountToUpdate);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}