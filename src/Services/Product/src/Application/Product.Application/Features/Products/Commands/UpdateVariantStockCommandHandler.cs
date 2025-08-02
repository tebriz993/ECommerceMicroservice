using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic; // KeyNotFoundException üçün
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class UpdateVariantStockCommandHandler : IRequestHandler<UpdateVariantStockCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVariantStockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateVariantStockCommand request, CancellationToken cancellationToken)
        {
            // Addım 1: Stoku yenilənəcək variantı bazadan tapırıq.
            var variant = await _unitOfWork.ProductRepository.GetVariantByIdAsync(request.VariantId);

            // Addım 2: Variantın mövcudluğunu yoxlayırıq.
            if (variant is null)
            {
                throw new KeyNotFoundException($"Product Variant with ID '{request.VariantId}' was not found.");
            }

            // Addım 3: BİZNES QAYDASI - Stokun mənfiyə düşməsinin qarşısını alırıq.
            // Əgər stokdan mal çıxılırsa (QuantityChange mənfidirsə)
            // və mövcud stokdan daha çox çıxılmağa cəhd edilirsə, xəta atırıq.
            if (request.QuantityChange < 0 && (variant.Quantity + request.QuantityChange < 0))
            {
                // Daha spesifik bir "ValidationException" və ya "BusinessRuleException" atmaq daha yaxşıdır.
                throw new InvalidOperationException($"Not enough stock available for variant '{variant.Name}'. Current stock: {variant.Quantity}");
            }

            // Addım 4: Stok miqdarını yeniləyirik.
            variant.Quantity += request.QuantityChange;

            // Addım 5: Repozitoridəki standart Update metodunu çağırırıq.
            _unitOfWork.ProductRepository.UpdateVariant(variant);

            // Addım 6: Dəyişikliyi bazaya yazırıq.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}