using AgricultureManager.Core.Application.Shared.Interfaces;

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
