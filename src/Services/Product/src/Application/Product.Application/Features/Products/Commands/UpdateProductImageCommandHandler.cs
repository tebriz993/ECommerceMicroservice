using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            var imageToUpdate = await _unitOfWork.ProductRepository.GetImageByIdAsync(request.ImageId);
            if (imageToUpdate is null)
                throw new KeyNotFoundException($"Image with ID '{request.ImageId}' not found.");

            if (request.UpdateDto.IsMainImage && !imageToUpdate.IsMainImage)
            {
                var otherImages = await _unitOfWork.ProductRepository.GetImagesByProductIdAsync(imageToUpdate.ProductId);
                foreach (var otherImage in otherImages)
                {
                    if (otherImage.IsMainImage)
                    {
                        otherImage.IsMainImage = false;
                        _unitOfWork.ProductRepository.UpdateImage(otherImage);
                    }
                }
            }

            // AutoMapper ilə DTO-dakı məlumatları mövcud entity üzərinə yazırıq.
            _mapper.Map(request.UpdateDto, imageToUpdate);

            // DÜZƏLİŞ: Artıq birbaşa repozitori metodunu çağırırıq.
            _unitOfWork.ProductRepository.UpdateImage(imageToUpdate);

            // Bütün dəyişiklikləri (həm bu şəklin, həm də digərlərinin statusu)
            // bir əməliyyatla bazaya yazırıq.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}