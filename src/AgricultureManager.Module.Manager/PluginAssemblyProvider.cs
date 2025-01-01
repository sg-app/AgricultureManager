using System.Reflection;

namespace AgricultureManager.Module.Manager
{
    public class PluginAssemblyProvider
    {
        public Assembly[] AdditionalPluginAssembliesWithRoutes { get; set; } = [];
        public Assembly[] AllPluginAssemblies { get; set; } = [];
    }
}
