using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class AddVariantsToProductCommandHandler : IRequestHandler<AddVariantsToProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddVariantsToProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(AddVariantsToProductCommand request, CancellationToken cancellationToken)
        {
            // Addım 1: Məhsulun mövcudluğunu yoxlayırıq.
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with ID '{request.ProductId}' was not found.");
            }

            // Addım 2: Gələn DTO siyahısını Entity siyahısına çeviririk.
            var variantsToAdd = _mapper.Map<List<ProductVariant>>(request.Variants);

            // Addım 3: Hər bir yeni variant üçün ProductId-ni təyin edib, repozitoriyə əlavə edirik.
            foreach (var variant in variantsToAdd)
            {
                variant.ProductId = product.Id;

                // Birləşdirilmiş ProductRepository-dəki xüsusi metodu çağırırıq.
                await _unitOfWork.ProductRepository.AddVariantAsync(variant);
            }

            // Addım 4: Bütün yeni variantları bir əməliyyatla bazaya yazırıq.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}