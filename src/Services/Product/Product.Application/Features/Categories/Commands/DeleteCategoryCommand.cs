using MediatR;

namespace Product.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}