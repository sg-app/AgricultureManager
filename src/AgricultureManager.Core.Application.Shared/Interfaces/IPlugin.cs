using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgricultureManager.Core.Application.Shared.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }

        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}
