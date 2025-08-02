using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Infrastructure;
using static MassTransit.MessageHeaders;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Infrastructure qatındakı bütün servisləri (IEmailService) qeydiyyatdan keçiririk.
                services.AddInfrastructureServices();

                // MassTransit və Consumer-lərin qeydiyyatı da burada olacaq...
                // services.AddMassTransit(...)
            });
}