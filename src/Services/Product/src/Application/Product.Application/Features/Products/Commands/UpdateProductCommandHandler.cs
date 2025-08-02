using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using System.Collections.Generic; // KeyNotFoundException üçün
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Məhsulu bazadan tapırıq. trackChanges: true olmalıdır ki, EF Core dəyişiklikləri izləsin.
            var productToUpdate = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            if (productToUpdate is null)
                throw new KeyNotFoundException($"Product with ID '{request.Id}' not found.");

            // AutoMapper, DTO-dakı dəyişiklikləri mövcud entity-nin üzərinə yazır.
            _mapper.Map(request.UpdateProductDto, productToUpdate);

            _unitOfWork.ProductRepository.Update(productToUpdate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}