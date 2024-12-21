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

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase)).ToList();
            assemblies.Add(Assembly.GetAssembly(typeof(CompanyState))!);

            services.AddFluxor(config =>
            {
                config.ScanAssemblies(
                    Assembly.GetExecutingAssembly(),
                    [
                        ..assemblies
                    ]);
#if DEBUG
                config.UseReduxDevTools();
#endif
            });

            //            services.AddFluxor(config =>
            //            {
            //                config.ScanAssemblies(
            //                    Assembly.GetExecutingAssembly(),
            //                    [
            //                        Assembly.GetAssembly(typeof(CompanyState)),
            //                    ]);
            //#if DEBUG
            //                config.UseReduxDevTools();
            //#endif
            //            });

            return services;
        }
    }
}
