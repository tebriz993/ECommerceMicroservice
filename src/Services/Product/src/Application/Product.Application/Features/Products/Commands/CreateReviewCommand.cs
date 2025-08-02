using MediatR;
using Product.Application.Dtos.Review;
using System;

namespace Product.Application.Features.Reviews.Commands
{
    // Cavab olaraq yaradılmış rəyin ID-sini qaytarırıq.
    public class CreateReviewCommand : IRequest<Guid>
    {
        public CreateReviewDto ReviewDto { get; set; }
    }
}