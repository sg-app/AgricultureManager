using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCorePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration.GetConnectionString("default");

            services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseMySql(conString, ServerVersion.AutoDetect(conString));
                options.EnableSensitiveDataLogging(configuration.GetValue("DbSettings:EnableSensitiveDataLogging", false));
            });

            services.AddScoped<IAppDbContextFactory, AppDbContextFactory>();

            services.AddDataProtection()
                .PersistKeysToDbContext<AppDbContext>();

            return services;
        }
    }
}
