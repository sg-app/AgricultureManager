using AgricultureManager.Module.Accounting.Persistence;
using AgricultureManager.Module.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgricultureManager.Module.Accounting
{
    public class ModuleRegistration : IServerStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(currentAssembly);
            });

            services.AddAutoMapper(currentAssembly);

            var conString = configuration.GetConnectionString("default");
            services.AddDbContextFactory<AccountingDbContext>(options =>
            {
                options.UseMySql(conString, ServerVersion.AutoDetect(conString), options =>
                {
                    options.MigrationsHistoryTable("__EFMigrationsHistory_Accounting");
                });
                options.EnableSensitiveDataLogging(configuration.GetValue("DbSettings:EnableSensitiveDataLogging", false));
            });
            services.AddScoped<IAccountingDbContextFactory, AccountingDbContextFactory>();

        }
    }
}
