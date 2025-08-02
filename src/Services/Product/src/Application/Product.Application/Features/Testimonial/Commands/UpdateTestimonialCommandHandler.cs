using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Testimonial.Commands
{
    public class UpdateTestimonialCommandHandler : IRequestHandler<UpdateTestimonialCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTestimonialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateTestimonialCommand request, CancellationToken cancellationToken)
        {
            var testimonialToUpdate = await _unitOfWork.TestimonialRepository.GetByIdAsync(request.Id);
            if (testimonialToUpdate is null)
                throw new KeyNotFoundException($"Testimonial with ID '{request.Id}' not found.");

            _mapper.Map(request.UpdateDto, testimonialToUpdate);

            _unitOfWork.TestimonialRepository.Update(testimonialToUpdate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}