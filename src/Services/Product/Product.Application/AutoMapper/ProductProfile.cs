// Product.Application/Mappings/ProductProfile.cs
using AutoMapper;
using Product.Application.Dtos.Product;
using Product.Domain.Entities;

namespace Product.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Entity to DTO mappings
            CreateMap<Products, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(src => src.Reviews.Count));

            CreateMap<Products, ProductDetailsDto>()
                .IncludeBase<Products, ProductDto>();

            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductTag, ProductTagDto>();
            CreateMap<Review, ProductReviewDto>();
            CreateMap<Products, RelatedProductDto>()
                .ForMember(dest => dest.MainImageUrl,
                    opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsMainImage).ImageUrl));

            // DTO to Entity mappings
            CreateMap<CreateProductDto, Products>();
            CreateMap<UpdateProductDto, Products>();
            CreateMap<CreateProductImageDto, ProductImage>();
            CreateMap<CreateProductVariantDto, ProductVariant>();
            CreateMap<UpdateProductImageDto, ProductImage>();
        }
    }
}