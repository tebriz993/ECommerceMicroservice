using AutoMapper;
using Product.Application.Dtos.Category;
using Product.Domain.Entities;

namespace Product.Application.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // --- Entity to DTO Mappings ---

            // Əsas çevirmə: Category -> CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ParentCategoryName,
                    opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null));

            // Siyahı üçün çevirmə: Category -> CategoryListItemDto
            CreateMap<Category, CategoryListItemDto>()
                .ForMember(dest => dest.ProductCount,
                    opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0));

            // Miras alan DTO-lar üçün IncludeBase istifadə edirik ki, təkrarçılıq olmasın.
            CreateMap<Category, CategoryWithProductsDto>().IncludeBase<Category, CategoryDto>();
            CreateMap<Category, CategoryWithChildsDto>().IncludeBase<Category, CategoryDto>();
            CreateMap<Category, CategoryTreeDto>().IncludeBase<Category, CategoryDto>();
            CreateMap<Category, CategoryDetailsDto>().IncludeBase<Category, CategoryDto>();

            // --- DTO to Entity Mappings (Commands üçün) ---

            // Create
            CreateMap<CreateCategoryDto, Category>();

            // Update
            CreateMap<UpdateCategoryDto, Category>()
                // Update zamanı ID-ni map etməyə ehtiyac yoxdur, çünki o, mövcud obyektə aiddir.
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}