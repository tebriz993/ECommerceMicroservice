using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Infrastructure.Persistence.Context;
using Product.Persistence.Repositories;
using Product.Persistence.Repositories.Common;
using Product.Application.Interfaces.Base;
using Product.Application.Interfaces;

namespace Product.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. DbContext
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ProductDbConnection")));

        // 2. Repozitorilər və UnitOfWork
        // DÜZƏLİŞ: Bütün repozitorilər burada qeydiyyatdan keçməlidir!
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductTagRepository, ProductTagRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<ITestimonialRepository, TestimonialRepository>();

        // ... digər repositorilər

        // UnitOfWork qeydiyyatı (DI container ona yuxarıdakı repozitoriləri ötürəcək)
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}