using AutoMapper;
using MediatR;
using Product.Application.Dtos.Product;
using Product.Application.Interfaces;
using System.Collections.Generic; // KeyNotFoundException üçün
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Queries
{
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductDetailsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            // IProductRepository-dəki spesifik metodu çağırırıq.
            var product = await _unitOfWork.ProductRepository
                .GetProductWithDetailsByIdAsync(request.Id, trackChanges: false);

            if (product is null)
            {
                // Application qatında yaradılmış xüsusi bir NotFoundException atmaq daha yaxşıdır.
                throw new KeyNotFoundException($"Product with ID '{request.Id}' not found.");
            }

            // AutoMapper vasitəsilə Entity-ni DTO-ya çeviririk.
            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}