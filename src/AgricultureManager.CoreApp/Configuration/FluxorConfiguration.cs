using AgricultureManager.Core.Application.Shared.States;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using System.Reflection;

namespace AgricultureManager.CoreApp.Configuration
{
    public static class FluxorConfiguration
    {
        public static IServiceCollection AddFluxorRegistration(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase))
                .ToList();
            assemblies.Add(Assembly.GetAssembly(typeof(HarvestYearState))!);

            services.AddFluxor(config =>
            {
                config.ScanAssemblies(
                    Assembly.GetExecutingAssembly(),
                    [.. assemblies]
                );
#if DEBUG
                config.UseReduxDevTools();
#endif
            });

            return services;
        }
    }
}
