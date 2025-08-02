using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Interfaces;
using Notification.Infrastructure.Services;

namespace Notification.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}