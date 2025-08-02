using AutoMapper;
using MediatR;
using Product.Application.Dtos.Product;
using Product.Application.Interfaces;
using Product.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Queries
{
    public class GetProductsByPageQueryHandler : IRequestHandler<GetProductsByPageQuery, PagedResponse<IReadOnlyList<ProductDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsByPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IReadOnlyList<ProductDto>>> Handle(GetProductsByPageQuery request, CancellationToken cancellationToken)
        {
            // 1. Repozitoridəki mürəkkəb filterləmə metodunu çağırırıq.
            var (products, totalCount) = await _unitOfWork.ProductRepository.GetProductsByPageAsync(request);

            // 2. Nəticəni DTO-ya çeviririk.
            var productDtos = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            // 3. Standart PagedResponse formatında qaytarırıq.
            return new PagedResponse<IReadOnlyList<ProductDto>>(productDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}