using AutoMapper;
using Product.Application.Dtos.Discount;
using Product.Domain.Entities;

namespace Product.Application.AutoMapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountDto>();

            // Detailed mapping, inheriting the base mapping and including related entities.
            CreateMap<Discount, DiscountDetailsDto>()
                .IncludeBase<Discount, DiscountDto>();


            // --- DTO to Entity Mappings (for Commands) ---

            // For creating a new discount.
            CreateMap<CreateDiscountDto, Discount>()
                // We ignore the ICollection properties because we will handle them manually in the handler
                // by fetching entities from the database based on their IDs.
                .ForMember(dest => dest.ApplicableProducts, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicableCategories, opt => opt.Ignore());

            // For updating an existing discount.
            CreateMap<UpdateDiscountDto, Discount>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Don't map the ID during an update
                .ForMember(dest => dest.ApplicableProducts, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicableCategories, opt => opt.Ignore());
        }
    }
}