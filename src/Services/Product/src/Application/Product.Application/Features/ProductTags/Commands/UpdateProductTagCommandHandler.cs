using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.ProductTags.Commands
{
    public class UpdateProductTagCommandHandler : IRequestHandler<UpdateProductTagCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductTagCommand request, CancellationToken cancellationToken)
        {
            var tagToUpdate = await _unitOfWork.ProductTagRepository.GetByIdAsync(request.Id);
            if (tagToUpdate is null)
                throw new KeyNotFoundException($"Tag with ID '{request.Id}' not found.");

            // Yeniləmə zamanı da adın unikallığını yoxlayırıq.
            var existingTagWithSameName = await _unitOfWork.ProductTagRepository.GetByNameAsync(request.UpdateTagDto.Name);
            if (existingTagWithSameName != null && existingTagWithSameName.Id != request.Id)
            {
                throw new InvalidOperationException($"Another tag with the name '{request.UpdateTagDto.Name}' already exists.");
            }

            _mapper.Map(request.UpdateTagDto, tagToUpdate);

            _unitOfWork.ProductTagRepository.Update(tagToUpdate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}