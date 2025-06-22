using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class DeleteVariantCommandHandler : IRequestHandler<DeleteVariantCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVariantCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
        {
            // Birləşdirilmiş repozitoriyə yeni bir metod əlavə etməliyik: DeleteVariant
            var variant = await _unitOfWork.ProductRepository.GetVariantByIdAsync(request.VariantId);
            if (variant == null)
                throw new KeyNotFoundException($"Variant with ID '{request.VariantId}' not found.");

            // IProductRepository-yə yeni bir metod əlavə edib burada çağırmalıyıq:
            // void DeleteVariant(ProductVariant variant);
            _unitOfWork.ProductRepository.DeleteVariant(variant);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}