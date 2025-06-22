using AutoMapper;
using Product.Application.Dtos.Product;
using Product.Application.Dtos.Review; // CreateReviewDto üçün
using Product.Domain.Entities;
using System.Linq;

namespace Product.Application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // --- Entity to DTO Mappings ---

            // Əsas çevirmə: Products -> ProductDto
            CreateMap<Products, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0));

            // Detallı səhifə üçün
            CreateMap<Products, ProductDetailsDto>().IncludeBase<Products, ProductDto>();

            // Əlaqəli entity-lər üçün çevirmələr
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductTag, ProductTagDto>();
            CreateMap<Review, CreateReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName)); // Review entity-dəki UserName-i map edirik.

            // "RelatedProducts" üçün xüsusi çevirmə
            CreateMap<Products, RelatedProductDto>()
                .ForMember(dest => dest.MainImageUrl,
                    opt => opt.MapFrom(src => src.Images != null && src.Images.Any(i => i.IsMainImage)
                        ? src.Images.First(i => i.IsMainImage).ImageUrl
                        : (src.Images != null && src.Images.Any() ? src.Images.First().ImageUrl : null)));

            // --- DTO to Entity Mappings (Commands üçün) ---

            // Create
            CreateMap<CreateProductDto, Products>();
            CreateMap<CreateProductImageDto, ProductImage>();
            CreateMap<CreateProductVariantDto, ProductVariant>();
            CreateMap<CreateReviewDto, Review>();

            // Update
            CreateMap<UpdateProductDto, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateProductImageDto, ProductImage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}