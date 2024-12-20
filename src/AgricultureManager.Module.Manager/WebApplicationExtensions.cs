using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgricultureManager.Module.Manager
{
    public static class WebApplicationExtensions
    {
        public static RazorComponentsEndpointConventionBuilder AddPluginAssemblies(this RazorComponentsEndpointConventionBuilder builder, IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            var pluginAssemblyProvider = serviceProvider.GetRequiredService<PluginAssemblyProvider>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase))
                .Where(assembly => assembly.GetTypes().Any(type => type.IsSubclassOf(typeof(ComponentBase)) && type.GetCustomAttribute<RouteAttribute>() != null))
                .ToArray();
            pluginAssemblyProvider.AdditionalPluginAssembliesWithRoutes = assemblies;
            builder.AddAdditionalAssemblies(assemblies);
            return builder;
        }
    }
}
