using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductImageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _unitOfWork.ProductRepository.GetImageByIdAsync(request.ImageId);
            if (image == null)
                throw new KeyNotFoundException($"Image with ID '{request.ImageId}' not found.");

            _unitOfWork.ProductRepository.DeleteImage(image);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}