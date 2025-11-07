using AgricultureManager.Core.Application.Shared.Interfaces;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Core.Application.Shared.Keys;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using System.Collections.Concurrent;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Services
{
    public class MasterdataService(IServiceProvider serviceProvider, ILogger<MasterdataService> logger) : IMasterdataService
    {
        private readonly Dictionary<Type, Func<Task>> _entityLoaderMap = [];
        private readonly ConcurrentDictionary<Type, object> _data = [];

        public void Register<TEntity, TViewModel>()
            where TEntity : class
            where TViewModel : class
        {
            _entityLoaderMap[typeof(TViewModel)] = () => LoadAsync<TEntity, TViewModel>();
        }

        private void RegisterCompany()
        {
            _entityLoaderMap[typeof(CompanyVm)] = () => LoadCompanyAsync();
        }

        public List<T> Get<T>() where T : class
        {
            if (_data.TryGetValue(typeof(T), out var data))
                return data as List<T> ?? [];

            return [];
        }

        private async Task LoadAsync<TEntity, TViewModel>() where TEntity : class where TViewModel : class
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var dbContextFactory = scope.ServiceProvider.GetRequiredService<IAppDbContextFactory>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                using var dbContext = dbContextFactory.CreateDbContext();
                var data = await dbContext.Set<TEntity>()
                    .AsNoTracking()
                    .ToListAsync();
                var viewModels = mapper.Map<List<TViewModel>>(data);
                Set(viewModels);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fehler beim Laden der Stammdaten für {Entity}", typeof(TEntity).Name);
            }
        }

        private async Task LoadCompanyAsync()
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var dbContextFactory = scope.ServiceProvider.GetRequiredService<IAppDbContextFactory>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                using var dbContext = dbContextFactory.CreateDbContext();
                var data = await dbContext.Parameter.FirstOrDefaultAsync(p => p.Key == ParameterKeys.Company);
                if (data == null || string.IsNullOrEmpty(data.Value))
                    return;

                var company = JsonSerializer.Deserialize<CompanyVm>(data.Value);
                if (company == null)
                    return;
                _data.AddOrUpdate(typeof(CompanyVm), company, (type, oldValue) => company);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fehler beim Laden der Company daten");
            }
        }

        private void Set<T>(List<T> value) where T : class
        {
            Type keyType = typeof(T);
            _data.AddOrUpdate(keyType, value, (type, oldValue) => value);
        }

        public async Task InitializeAsync()
        {
            Register<Field, FieldVm>();
            Register<Culture, CultureVm>();
            Register<SeedCategory, SeedCategoryVm>();
            Register<SeedTechnology, SeedTechnologyVm>();
            Register<Unit, UnitVm>();
            Register<Person, PersonVm>();
            Register<Fertilizer, FertilizerVm>();
            Register<FertilizerToDetail, FertilizerToDetailVm>();
            Register<FertilizerDetail, FertilizerDetailVm>();
            Register<PlantProtectant, PlantProtectantVm>();
            Register<HarvestYear, HarvestYearVm>();
            RegisterCompany();
            RegisterPlugins();

            var tasks = _entityLoaderMap
                .Select(pair => pair.Value())
                .ToList();

            await Task.WhenAll(tasks);
        }

        private void RegisterPlugins()
        {
            var loaderTypes = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(a => a.GetTypes())
               .Where(t => !t.IsAbstract && !t.IsInterface)
               .SelectMany(t => t.GetInterfaces()
                   .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMasterDataLoader<>))
                   .Select(i => new { ImplementationType = t, InterfaceType = i }))
               .ToList();

            foreach (var loader in loaderTypes)
            {
                var viewModelType = loader.InterfaceType.GenericTypeArguments[0];
                var registerMethod = typeof(MasterdataService).GetMethod("RegisterPluginLoader")?.MakeGenericMethod(viewModelType);
                registerMethod?.Invoke(this, null);
            }
        }

        public async Task LoadPluginMasterdataAsync<TViewModel>()
            where TViewModel : class
        {
            using var scope = serviceProvider.CreateScope();
            var loader = scope.ServiceProvider.GetRequiredService<IMasterDataLoader<TViewModel>>();
            var data = await loader.LoadDataAsync();
            Set(data);
        }

        public void RegisterPluginLoader<TViewModel>()
            where TViewModel : class
        {
            _entityLoaderMap[typeof(TViewModel)] = () => LoadPluginMasterdataAsync<TViewModel>();
        }

        public async Task ReloadAsync<T>() where T : class
        {
            var task = _entityLoaderMap[typeof(T)]();
            await task;
        }

        public CompanyVm GetCompany()
        {
            if (_data.TryGetValue(typeof(CompanyVm), out var data))
                return data as CompanyVm ?? default!;

            return default!;
        }
    }
}
