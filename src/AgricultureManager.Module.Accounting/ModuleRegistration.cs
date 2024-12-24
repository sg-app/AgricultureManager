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

            services.AddSingleton<IMenuItem, AccountingMenuItem>();
        }
    }

    public class AccountingMenuItem : IMenuItem
    {
        public string Name => "Accounting";
        public string Icon => "money";
        public string Url => string.Empty;

        public IEnumerable<IMenuItem> Children => [
            new AccountingOverviewMenuItem(),
            new StatementOfAccountMenuItem()
            ];
    }

    public class AccountingOverviewMenuItem : IMenuItem
    {
        public string Name => "Buchungen";
        public string Icon => "money";
        public string Url => "/accounting/overview";

        public IEnumerable<IMenuItem> Children => [];
    }

    public class StatementOfAccountMenuItem : IMenuItem
    {
        public string Name => "Kontoauszug";
        public string Icon => "money";
        public string Url => "/accounting/statementofaccount";

        public IEnumerable<IMenuItem> Children => [];
    }
}
