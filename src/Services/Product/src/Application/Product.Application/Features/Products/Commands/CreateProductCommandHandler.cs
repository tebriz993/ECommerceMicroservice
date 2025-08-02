using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Domain.Entities.Products>(request.CreateProductDto);

            // Tag-ləri mövcud olanlardan ID ilə tapıb bağlayırıq.
            // Bu məntiqin burada olması doğrudur.
            if (request.CreateProductDto.TagIds != null && request.CreateProductDto.TagIds.Any())
            {
                var tags = await _unitOfWork.ProductTagRepository
                    .FindByConditionAsync(t => request.CreateProductDto.TagIds.Contains(t.Id));
                product.Tags = new List<ProductTag>(tags);
            }

            // DÜZƏLİŞ: Artıq düzgün metod çağırılır.
            await _unitOfWork.ProductRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}