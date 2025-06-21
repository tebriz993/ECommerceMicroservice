using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // 1. DTO-dan Entity-yə map edirik.
        var category = _mapper.Map<Category>(request.CreateCategoryDto);

        // 2. Repozitori vasitəsilə əlavə edirik (hələ bazaya yazılmır, sadəcə izlənilir).
        await _unitOfWork.CategoryRepository.AddAsync(category);

        // 3. Bütün dəyişiklikləri bir əməliyyatla (tranzaksiya ilə) bazaya yazırıq.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Yaradılmış obyektin ID-sini qaytarırıq.
        return category.Id;
    }
}