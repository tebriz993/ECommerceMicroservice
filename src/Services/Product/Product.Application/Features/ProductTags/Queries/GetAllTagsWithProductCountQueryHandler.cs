using AutoMapper;
using MediatR;
using Product.Application.Dtos.ProductTags;
using Product.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.ProductTags.Queries
{
    public class GetAllTagsWithProductCountQueryHandler : IRequestHandler<GetAllTagsWithProductCountQuery, IReadOnlyList<ProductTagWithProductCountDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTagsWithProductCountQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductTagWithProductCountDto>> Handle(GetAllTagsWithProductCountQuery request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.ProductTagRepository.GetAllWithProductCountAsync();

            return _mapper.Map<IReadOnlyList<ProductTagWithProductCountDto>>(tags);
        }
    }
}