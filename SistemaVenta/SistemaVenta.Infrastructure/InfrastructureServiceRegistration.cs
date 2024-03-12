using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.Infrastructure.Persistence;

namespace SistemaVenta.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SistemaVentaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
            );
            return services;
        }

    }
}
