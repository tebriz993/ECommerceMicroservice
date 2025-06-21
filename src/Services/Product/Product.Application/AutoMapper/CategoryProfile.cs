// Product.Application/Mappings/CategoryProfile.cs
using AutoMapper;
using Product.Application.Dtos.Category;
using Product.Domain.Entities;

namespace Product.Application.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ParentCategoryName,
                    opt => opt.MapFrom(src => src.ParentCategory.Name));

            CreateMap<Category, CategoryWithChildsDto>()
                .IncludeBase<Category, CategoryDto>();

            CreateMap<Category, CategoryWithProductsDto>()
                .IncludeBase<Category, CategoryDto>();

            CreateMap<Category, CategoryTreeDto>()
                .IncludeBase<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Category, CategoryListItemDto>()
            .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

        }
    }
}