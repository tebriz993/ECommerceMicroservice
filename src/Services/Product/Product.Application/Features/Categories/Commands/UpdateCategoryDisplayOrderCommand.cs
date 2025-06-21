using MediatR;

public class UpdateCategoryDisplayOrderCommand : IRequest
{
    // Dictionary<CategoryId, NewDisplayOrder>
    public Dictionary<Guid, int> CategoryOrders { get; set; }
}