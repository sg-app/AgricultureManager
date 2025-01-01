using AgricultureManager.Module.Api.Interfaces;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static IEnumerable<IMenuItem> GetMenuItems(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServices<IMenuItem>();
        }
    }
}
