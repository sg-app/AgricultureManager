using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgricultureManager.Core.Application.Shared.Interfaces
{
    public interface IServerStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        void AddMiddleware(IHost app) { }
    }
}
