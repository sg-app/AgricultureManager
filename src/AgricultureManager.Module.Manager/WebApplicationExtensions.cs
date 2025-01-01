using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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
                .Where(f => f.FullName?.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase) == true)
                .Where(assembly => assembly.GetTypes().Any(type => type.IsSubclassOf(typeof(ComponentBase)) && type.GetCustomAttribute<RouteAttribute>() != null))
                .ToArray();
            pluginAssemblyProvider.AdditionalPluginAssembliesWithRoutes = assemblies;
            builder.AddAdditionalAssemblies(assemblies);
            return builder;
        }

        public static void MigratePluginDatabase(this WebApplication app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(f => f.FullName?.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase) == true);
            foreach (var assembly in assemblies)
            {
                assembly.GetAssignableFrom<DbContext>()
                    .ToList()
                    .ForEach(x => DoMigration(x, app));
            }
        }

        private static void DoMigration(Type type, WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService(type) as DbContext;
            dbContext?.Database.Migrate();
        }

        private static IEnumerable<Type> GetAssignableFrom<T>(this Assembly assembly) where T : class
        {
            ArgumentNullException.ThrowIfNull(assembly);

            var type = typeof(T);
            return assembly.GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsGenericType && type.IsAssignableFrom(x));
        }
    }
}
