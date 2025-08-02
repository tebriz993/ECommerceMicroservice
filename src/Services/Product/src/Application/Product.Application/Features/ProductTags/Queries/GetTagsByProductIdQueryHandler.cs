using AutoMapper;
using MediatR;
using Product.Application.Dtos.ProductTags;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.ProductTags.Queries
{
    public class GetTagsByProductIdQueryHandler : IRequestHandler<GetTagsByProductIdQuery, IReadOnlyList<ProductTagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagsByProductIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductTagDto>> Handle(GetTagsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.ProductTagRepository.GetTagsByProductIdAsync(request.ProductId);

            return _mapper.Map<IReadOnlyList<ProductTagDto>>(tags);
        }
    }
}