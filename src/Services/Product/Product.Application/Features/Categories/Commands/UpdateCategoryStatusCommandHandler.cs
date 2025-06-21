using MediatR;
using Product.Application.Interfaces; // IUnitOfWork üçün
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic; // KeyNotFoundException üçün

namespace Product.Application.Features.Categories.Commands;

public class UpdateCategoryStatusCommandHandler : IRequestHandler<UpdateCategoryStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateCategoryStatusCommand request, CancellationToken cancellationToken)
    {
        // 1. Statusu dəyişdiriləcək kateqoriyanı bazadan tapırıq.
        var categoryToUpdate = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

        // 2. Əgər belə bir kateqoriya tapılmazsa, xəta atırıq.
        if (categoryToUpdate is null)
        {
            // Daha yaxşı bir yanaşma, Application qatında yaradılmış xüsusi bir
            // NotFoundException atmaqdır ki, API qatında 404 Not Found status kodu qaytarılsın.
            throw new KeyNotFoundException($"Category with ID '{request.Id}' was not found.");
        }

        // 3. Kateqoriyanın statusunu yeniləyirik.
        // Gələcəkdə bu məntiqi birbaşa Category entity-sinin içindəki bir metoda
        // (məsələn, categoryToUpdate.Activate() və ya categoryToUpdate.Deactivate()) daşıya bilərik.
        categoryToUpdate.IsActive = request.IsActive;

        // 4. Repozitoridə Update metodunu çağıraraq EF Core-a dəyişikliyi bildiririk.
        _unitOfWork.CategoryRepository.Update(categoryToUpdate);

        // 5. Dəyişikliyi verilənlər bazasına yazırıq.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}