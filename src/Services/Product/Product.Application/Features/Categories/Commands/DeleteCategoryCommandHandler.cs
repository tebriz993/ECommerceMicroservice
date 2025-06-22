using MediatR;
using Product.Application.Exceptions;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Categories.Commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToDelete = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

        if (categoryToDelete is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        // Burada biznes məntiqi ola bilər: Məsələn, içində məhsul olan kateqoriyanı silməyə icazə verməmək.
        // if (categoryToDelete.Products.Any()) { throw new Exception("Cannot delete category with products."); }

        _unitOfWork.CategoryRepository.Delete(categoryToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}