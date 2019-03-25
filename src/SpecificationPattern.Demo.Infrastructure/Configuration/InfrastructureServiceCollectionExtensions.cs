using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpecificationPattern.Demo.Infrastructure.Repositories;

namespace SpecificationPattern.Demo.Infrastructure.Configuration
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IReadAsyncRepository<>), typeof(ReadAsyncRepository<>));
            services.AddDataConnection(connectionString);

            return services;
        }

        private static IServiceCollection AddDataConnection(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
