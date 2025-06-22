using AutoMapper;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Testimonial.Commands
{
    public class CreateTestimonialCommandHandler : IRequestHandler<CreateTestimonialCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTestimonialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTestimonialCommand request, CancellationToken cancellationToken)
        {
            var testimonial = _mapper.Map<Domain.Entities.Testimonial>(request.CreateDto);
            await _unitOfWork.TestimonialRepository.AddAsync(testimonial);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return testimonial.Id;
        }
    }
}