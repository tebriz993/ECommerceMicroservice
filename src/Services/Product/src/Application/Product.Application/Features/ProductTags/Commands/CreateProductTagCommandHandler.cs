using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.ProductTags.Commands
{
    public class CreateProductTagCommandHandler : IRequestHandler<CreateProductTagCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductTagCommand request, CancellationToken cancellationToken)
        {
            // Biznes qaydası: Eyni adda ikinci bir teq yaratmağa icazə vermirik.
            var existingTag = await _unitOfWork.ProductTagRepository.GetByNameAsync(request.CreateTagDto.Name);
            if (existingTag != null)
            {
                // Xüsusi bir "DuplicateException" və ya "BusinessRuleException" atmaq daha yaxşıdır.
                throw new InvalidOperationException($"A tag with the name '{request.CreateTagDto.Name}' already exists.");
            }

            var tag = _mapper.Map<ProductTag>(request.CreateTagDto);

            await _unitOfWork.ProductTagRepository.AddAsync(tag);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag.Id;
        }
    }
}