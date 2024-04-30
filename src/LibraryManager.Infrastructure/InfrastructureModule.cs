using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryManager.Infrastructure.Persistence;
using LibraryManager.Infrastructure.Integrations;
using LibraryManager.Core.Integrations.ApiCepIntegration;
using LibraryManager.Infrastructure.Persistence.Repositories;

namespace LibraryManager.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LibraryManagerCs");

            services
                .AddDb(connectionString)
                .AddRepositories()
                .AddIntegrations();

            return services;
        }

        private static IServiceCollection AddDb(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<LibraryDbContext>(options =>
              options.UseSqlServer(connectionString, b => b.MigrationsAssembly("LibraryManager.Infrastructure")));

            return services;
        }


        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddIntegrations(this IServiceCollection services)
        {
            services.AddScoped<IApiCepService, ApiCepIntegration>();

            return services;
        }
    }
}
