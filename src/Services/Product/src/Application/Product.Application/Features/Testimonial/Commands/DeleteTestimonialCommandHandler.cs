using MediatR;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Testimonial.Commands
{
    public class DeleteTestimonialCommandHandler : IRequestHandler<DeleteTestimonialCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTestimonialCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTestimonialCommand request, CancellationToken cancellationToken)
        {
            var testimonialToDelete = await _unitOfWork.TestimonialRepository.GetByIdAsync(request.Id);
            if (testimonialToDelete is null)
                throw new KeyNotFoundException($"Testimonial with ID '{request.Id}' not found.");

            _unitOfWork.TestimonialRepository.Delete(testimonialToDelete);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}