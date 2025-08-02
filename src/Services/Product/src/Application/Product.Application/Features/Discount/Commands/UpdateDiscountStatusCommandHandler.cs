using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Discount.Commands
{
    public class UpdateDiscountStatusCommandHandler : IRequestHandler<UpdateDiscountStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDiscountStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateDiscountStatusCommand request, CancellationToken cancellationToken)
        {
            // Use the base repository method to find the discount.
            var discount = await _unitOfWork.DiscountRepository.GetByIdAsync(request.Id);

            if (discount is null)
                throw new KeyNotFoundException($"Discount with ID '{request.Id}' was not found.");

            // Update only the IsActive property.
            discount.IsActive = request.IsActive;

            // Mark the entity as updated.
            _unitOfWork.DiscountRepository.Update(discount);

            // Save the change to the database.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}