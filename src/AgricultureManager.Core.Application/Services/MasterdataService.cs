using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgricultureManager.Core.Application.Services
{
    public class MasterdataService(IServiceProvider serviceProvider) : IMasterdataService
    {
        private readonly Dictionary<Type, Func<Task>> _entityLoaderMap = [];
        private readonly Dictionary<Type, object> _data = [];

        public void Register<TEntity, TViewModel>()
            where TEntity : class
            where TViewModel : class
        {
            _entityLoaderMap[typeof(TViewModel)] = () => LoadAsync<TEntity, TViewModel>();
        }

        public List<T> Get<T>() where T : class
        {
            if (_data.TryGetValue(typeof(T), out var data))
                return data as List<T> ?? default!;

            return default!;
        }

        private async Task LoadAsync<TEntity, TViewModel>() where TEntity : class where TViewModel : class
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

        private void Set<T>(List<T> value) where T : class
        {
            Type keyType = typeof(T);
            _data.Remove(keyType);
            _data.Add(keyType, value);
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


            var tasks = _entityLoaderMap
                .Select(pair => pair.Value())
                .ToList();
            await Task.WhenAll(tasks);
        }

        public async Task ReloadAsync<T>() where T : class
        {
            var task = _entityLoaderMap[typeof(T)]();
            await task;
        }
    }
}
