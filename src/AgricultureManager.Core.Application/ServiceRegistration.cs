using AgricultureManager.Core.Application.Shared.States;
using FluentValidation;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgricultureManager.Core.Application
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddCoreApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddFluxor(config =>
            {
                config.ScanAssemblies(Assembly.GetExecutingAssembly(), typeof(CompanyState).Assembly);
#if DEBUG
                config.UseReduxDevTools();
#endif
            });

            return services;
        }
    }
}
