using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductTagRepository ProductTagRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        //add other repositories there...

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}