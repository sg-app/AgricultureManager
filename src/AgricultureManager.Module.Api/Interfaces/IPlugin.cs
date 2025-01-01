namespace AgricultureManager.Module.Api.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }

        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}
