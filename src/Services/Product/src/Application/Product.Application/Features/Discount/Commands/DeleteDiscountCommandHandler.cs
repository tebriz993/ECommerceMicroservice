using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Commands
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDiscountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountToDelete = await _unitOfWork.DiscountRepository.GetByIdAsync(request.Id);

            if (discountToDelete is null)
            {
                throw new KeyNotFoundException($"Discount with ID '{request.Id}' was not found.");
            }

            _unitOfWork.DiscountRepository.Delete(discountToDelete);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}