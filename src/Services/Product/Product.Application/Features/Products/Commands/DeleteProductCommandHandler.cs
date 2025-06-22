using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            if (productToDelete is null)
                throw new KeyNotFoundException($"Product with ID '{request.Id}' not found.");

            // BIZNES MƏNTİQİ: Əgər məhsul hər hansı bir aktiv sifarişdə varsa, silməyə icazə verməmək
            // üçün burada yoxlama aparıla bilər (bu, OrderingService ilə əlaqə tələb edəcək).
            // Hələlik sadə silmə əməliyyatı edirik.

            _unitOfWork.ProductRepository.Delete(productToDelete);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}