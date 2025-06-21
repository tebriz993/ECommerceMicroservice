using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Reviews.Commands
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            // Məhsulun mövcudluğunu yoxlamaq (opsional, amma yaxşı təcrübədir)
            var productExists = await _unitOfWork.ProductRepository.GetByIdAsync(request.ReviewDto.ProductId) != null;
            if (!productExists)
            {
                throw new KeyNotFoundException($"Product with ID '{request.ReviewDto.ProductId}' not found.");
            }

            var review = _mapper.Map<Review>(request.ReviewDto);

            // IReviewRepository-ni IUnitOfWork-a əlavə etməlisiniz.
            await _unitOfWork.ProductRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return review.Id;
        }
    }
}