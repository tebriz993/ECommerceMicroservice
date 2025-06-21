using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        // 1. Dəyişdiriləcək obyekti bazadan tapırıq.
        var categoryToUpdate = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

        // 2. Obyekt tapılmazsa, xəta atırıq. (NotFoundException daha yaxşıdır)
        if (categoryToUpdate is null)
        {
            throw new KeyNotFoundException($"Category with id {request.Id} not found.");
        }

        // 3. AutoMapper ilə DTO-dakı məlumatları mövcud entity üzərinə yazırıq.
        _mapper.Map(request.UpdateCategoryDto, categoryToUpdate);

        // 4. Repozitoridə Update metodunu çağırırıq (bu, sadəcə state-i "Modified" edir).
        _unitOfWork.CategoryRepository.Update(categoryToUpdate);

        // 5. Dəyişikliyi bazaya yazırıq.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}