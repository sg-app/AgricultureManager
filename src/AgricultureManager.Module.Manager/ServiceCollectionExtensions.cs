using AgricultureManager.Module.Api.Interfaces;
using AgricultureManager.Module.Manager;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlugins(this IServiceCollection services, IConfiguration configuration)
        {
            LoadAsseblies();
            var pluginAssemblyProvider = new PluginAssemblyProvider();
            services.AddSingleton(pluginAssemblyProvider);
            services.AddPluginServices(configuration, pluginAssemblyProvider);
            //services.AddCustomComponents<CommonSettingsComponentBase>();
            //services.AddCustomComponents<SettingsComponentBase>();


            return services;
        }
        private static void LoadAsseblies()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (assemblyPath == null) return;

            AssemblyLoadContext.Default.Resolving += ResolveDependencies;

            var assembliesFolder = new DirectoryInfo(assemblyPath);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var dlls = assembliesFolder.EnumerateFiles($"*.dll", SearchOption.AllDirectories);

            foreach (var dll in dlls)
            {
                AssemblyName assemblyName;
                try
                {
                    assemblyName = AssemblyName.GetAssemblyName(dll.FullName);
                }
                catch
                {
                    Debug.WriteLine($"Cannot Get Assembly Name For {dll.Name}");
                    continue;
                }

                if (!assemblies.Any(a => AssemblyName.ReferenceMatchesDefinition(assemblyName, a.GetName())))
                {

                    var pdb = Path.ChangeExtension(dll.FullName, ".pdb");
                    Assembly? assembly = null;

                    // load assembly ( and symbols ) from stream to prevent locking files ( as long as dependencies are in /bin they will load as well )
                    if (File.Exists(pdb))
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(File.ReadAllBytes(dll.FullName)), new MemoryStream(File.ReadAllBytes(pdb)));
                    }
                    else
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(File.ReadAllBytes(dll.FullName)));
                    }

                }
            }
        }

        private static Assembly? ResolveDependencies(AssemblyLoadContext context, AssemblyName name)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + Path.DirectorySeparatorChar + name.Name + ".dll";
            if (File.Exists(assemblyPath))
            {
                return context.LoadFromStream(new MemoryStream(File.ReadAllBytes(assemblyPath)));
            }
            else
            {
                return null;
            }
        }

        private static IServiceCollection AddPluginServices(this IServiceCollection services, IConfiguration configuration, PluginAssemblyProvider pluginAssemblyProvider)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));


            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase));
            pluginAssemblyProvider.AllPluginAssemblies = assemblies.ToArray();

            foreach (var assembly in assemblies)
            {
                // dynamically register server startup services
                assembly.GetInstances<IServerStartup>()
                    .ToList()
                    .ForEach(x => x.ConfigureServices(services, configuration));
            }

            return services;
        }

        //private static IServiceCollection AddCommonSettingPages(this IServiceCollection services)
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }


        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase));

        //    foreach (var assembly in assemblies)
        //    {
        //        assembly.GetTypesOf<CommonSettingsComponentBase>()
        //            .ToList()
        //            .ForEach(x => services.AddSingleton(typeof(CommonSettingsComponentBase), x));

        //    }
        //    return services;
        //}

        //private static IServiceCollection AddSettingPages(this IServiceCollection services)
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }


        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase));

        //    foreach (var assembly in assemblies)
        //    {
        //        assembly.GetTypesOf<SettingsComponentBase>()
        //            .ToList()
        //            .ForEach(x => services.AddSingleton(typeof(SettingsComponentBase), x));

        //    }
        //    return services;
        //}
        //private static IServiceCollection AddCustomComponents<T>(this IServiceCollection services)
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }


        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase));

        //    foreach (var assembly in assemblies)
        //    {
        //        assembly.GetTypesOf<T>()
        //            .ToList()
        //            .ForEach(x => services.AddSingleton(typeof(T), x));

        //    }
        //    return services;
        //}

        //private static IServiceCollection AddTabPages(this IServiceCollection services)
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }


        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase));

        //    foreach (var assembly in assemblies)
        //    {
        //        assembly.GetTypesOf<TabComponentBase>()
        //            .ToList()
        //            .ForEach(x => services.AddSingleton(typeof(TabComponentBase), x));

        //    }
        //    return services;
        //}

        public static IEnumerable<T> GetInstances<T>(this Assembly assembly) where T : class
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var type = typeof(T);
            var list = assembly.GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsGenericType && type.IsAssignableFrom(x));

            foreach (var type1 in list)
            {
                if (Activator.CreateInstance(type1) is T instance) yield return instance;
            }
        }
        public static IEnumerable<Type> GetTypesOf<T>(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var type = typeof(T);
            var list = assembly.GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsGenericType && type.IsAssignableFrom(x));

            return list;
        }
    }
}
