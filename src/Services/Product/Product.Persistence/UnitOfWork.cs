using Product.Application.Interfaces;
using Product.Infrastructure.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Persistence; // Faylın yerləşdiyi layihəyə uyğun

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductDbContext _dbContext;

    public UnitOfWork(
        ProductDbContext dbContext,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IProductTagRepository productTagRepository,
        IReviewRepository reviewRepository,
        IDiscountRepository discountRepository,
        ITestimonialRepository testimonialRepository)
    {
        _dbContext = dbContext;
        ProductRepository = productRepository;
        CategoryRepository = categoryRepository;
        ProductTagRepository = productTagRepository;
        ReviewRepository = reviewRepository;
        DiscountRepository = discountRepository;
        TestimonialRepository = testimonialRepository;
    }


    // Property-lər DI-dən gələn instansiyaları saxlayır.
    public IProductRepository ProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IProductTagRepository ProductTagRepository { get; }
    public IReviewRepository ReviewRepository { get; }
    public IDiscountRepository DiscountRepository { get; }
    public ITestimonialRepository TestimonialRepository { get; }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}