using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductTagRepository ProductTagRepository { get; }
        IProductVariantRepository ProductVariantRepository { get; }
        IReviewRepository ReviewRepository { get; }
        //add other repositories there...

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}