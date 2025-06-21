using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Infrastructure
{

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Hələlik boşdur.
            // Gələcəkdə bura əlavə olunacaq:
            // services.AddTransient<IEmailSender, EmailSender>();
            // services.AddSingleton<ICacheService, RedisCacheService>();

            return services;
        }
    }
}