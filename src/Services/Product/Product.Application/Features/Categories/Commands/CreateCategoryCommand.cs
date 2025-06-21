using MediatR;
using Product.Application.Dtos;
using Product.Application.Dtos.Category;

namespace Product.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public CreateCategoryDto CreateCategoryDto { get; set; }
    }
}