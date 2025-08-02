using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.ProductTags.Commands
{
    public class DeleteProductTagCommandHandler : IRequestHandler<DeleteProductTagCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductTagCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductTagCommand request, CancellationToken cancellationToken)
        {
            var tagToDelete = await _unitOfWork.ProductTagRepository.GetByIdAsync(request.Id);
            if (tagToDelete is null)
                throw new KeyNotFoundException($"Tag with ID '{request.Id}' not found.");

            // BİZNES MƏNTİQİ: Əgər bu teq hər hansı bir məhsulda istifadə olunursa,
            // onu silməyə icazə verməmək olar. Bunun üçün repozitoriyə yeni metod lazım olacaq.
            // Hələlik sadə silmə edirik.

            _unitOfWork.ProductTagRepository.Delete(tagToDelete);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}