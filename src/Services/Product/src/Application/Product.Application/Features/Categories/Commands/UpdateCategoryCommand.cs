using MediatR;
using Product.Application.Dtos;
using Product.Application.Dtos.Category;

namespace Product.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        public UpdateCategoryDto UpdateCategoryDto { get; set; }
    }
}
