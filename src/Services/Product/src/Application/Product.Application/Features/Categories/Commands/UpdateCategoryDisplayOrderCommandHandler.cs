using MediatR;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Commands;

public class UpdateCategoryDisplayOrderCommandHandler : IRequestHandler<UpdateCategoryDisplayOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryDisplayOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateCategoryDisplayOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Gələn sorğudakı bütün kateqoriya ID-lərini bir siyahıya yığırıq.
        var categoryIds = request.CategoryOrders.Keys.ToList();

        if (!categoryIds.Any())
        {
            // Əgər heç bir ID göndərilməyibsə, heç bir iş görmədən qayıdırıq.
            return;
        }

        // 2. Bütün lazımi kateqoriyaları verilənlər bazasından BİR SORĞUDA çəkirik.
        // Bu, hər bir kateqoriya üçün ayrı-ayrı `GetByIdAsync` çağırmaqdan qat-qat daha effektivdir.
        var categoriesToUpdate = await _unitOfWork.CategoryRepository
            .FindByConditionAsync(c => categoryIds.Contains(c.Id), trackChanges: true);
        // trackChanges: true olmalıdır, çünki bu obyektləri dəyişəcəyik.

        // 3. Kateqoriyaları asan müraciət üçün bir Dictionary-yə çeviririk.
        var categoriesDictionary = categoriesToUpdate.ToDictionary(c => c.Id);

        // 4. Hər bir kateqoriyanın `DisplayOrder` dəyərini yeniləyirik.
        foreach (var orderInfo in request.CategoryOrders)
        {
            // Əgər bazadan çəkdiyimiz kateqoriyalar arasında bu ID varsa...
            if (categoriesDictionary.TryGetValue(orderInfo.Key, out var category))
            {
                // ... onun DisplayOrder-ni yenisi ilə əvəz edirik.
                // Bu dəyişiklik EF Core tərəfindən izlənilir.
                category.DisplayOrder = orderInfo.Value;
            }
            // Əgər göndərilən ID bazada tapılmazsa, onu görməzdən gələ bilərik və ya xəta ata bilərik.
            // Hazırkı implementasiyada görməzdən gəlirik.
        }

        // 5. Bütün dəyişiklikləri bir əməliyyatla (tranzaksiya ilə) bazaya yazırıq.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}