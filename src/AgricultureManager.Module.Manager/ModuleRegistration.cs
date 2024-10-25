using AgricultureManager.Module.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace AgricultureManager.Module.Manager
{
    public static class ModuleRegistration
    {
        public static IServiceCollection RegisterMasterdata(this IServiceCollection services)
        {

            var masterdataTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(m => m.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IMasterdata)));

            foreach (var type in masterdataTypes)
            {
                services.AddSingleton(typeof(IMasterdata), type);
            }

            return services;
        }
    }
}
