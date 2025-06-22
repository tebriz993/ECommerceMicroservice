using Product.Application.Dtos.Category;
using Product.Application.Dtos.Product;
using System.Collections.Generic;

namespace Product.Application.Dtos.Discount
{
    public class DiscountDetailsDto : DiscountDto
    {
        // Yalnız ad və ID kimi minimal məlumatları saxlamaq üçün yeni DTO-lar yarada bilərik,
        // amma başlanğıc üçün mövcud ProductDto və CategoryDto-dan istifadə edək.
        public IReadOnlyList<ProductDto> ApplicableProducts { get; set; } = new List<ProductDto>();
        public IReadOnlyList<CategoryDto> ApplicableCategories { get; set; } = new List<CategoryDto>();
    }
}