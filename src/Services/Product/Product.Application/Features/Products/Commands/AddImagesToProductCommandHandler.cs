using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class AddImagesToProductCommandHandler : IRequestHandler<AddImagesToProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddImagesToProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(AddImagesToProductCommand request, CancellationToken cancellationToken)
        {
            // Addım 1: Məhsulun mövcudluğunu yoxlayırıq.
            // GetProductByIdAsync istifadə edirik ki, məhsulun özünə ehtiyacımız yoxdur, sadəcə varlığını bilmək kifayətdir.
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                // Məhsul tapılmazsa, xəta atırıq.
                throw new KeyNotFoundException($"Product with ID '{request.ProductId}' was not found.");
            }

            // Addım 2: Gələn DTO siyahısını Entity siyahısına çeviririk.
            var imagesToAdd = _mapper.Map<List<ProductImage>>(request.Images);

            // Addım 3: Hər bir yeni şəkil üçün ProductId-ni təyin edib, repozitoriyə əlavə edirik.
            foreach (var image in imagesToAdd)
            {
                // Hər bir yeni şəklin hansı məhsula aid olduğunu bildiririk.
                image.ProductId = product.Id;

                // Birləşdirilmiş ProductRepository-dəki xüsusi metodu çağırırıq.
                await _unitOfWork.ProductRepository.AddImageAsync(image);
            }

            // Addım 4: Bütün yeni şəkilləri bir əməliyyatla bazaya yazırıq.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}