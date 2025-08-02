using AutoMapper;
using Product.Application.Dtos.ProductTags;
using Product.Domain.Entities;

namespace Product.Application.AutoMapper
{
    public class ProductTagsProfile : Profile
    {
        public ProductTagsProfile()
        {
            CreateMap<ProductTag, ProductTagDto>();

            CreateMap<ProductTag, ProductTagWithProductCountDto>()
                .ForMember(dest => dest.ProductCount,
                           opt => opt.MapFrom(src => src.Products.Count));

            CreateMap<CreateProductTagDto, ProductTag>();

            CreateMap<UpdateProductTagDto, ProductTag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}