using AgricultureManager.Core.Application.Common;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Services
{
    public class HarvestYearService(IServiceProvider serviceProvider) : IHarvestYearService
    {
        private HarvestYearVm? _selectedHarvestYear;

        public event EventHandler<HarvestYearVm> HarvestYearChanged = delegate { };

        public async ValueTask<HarvestYearVm?> GetSelectedYearAsync()
        {
            if (_selectedHarvestYear is null)
            {
                var dbContextFactory = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IAppDbContextFactory>();
                using var dbContext = dbContextFactory.CreateDbContext();
                var cancellationToken = CancellationToken.None;

                var param = await dbContext.Parameter.FindAsync([ParameterKeys.SelectedHarvestYear], cancellationToken);
                if (param is not null)
                {
                    _selectedHarvestYear = JsonSerializer.Deserialize<HarvestYearVm>(param.Value);
                    return _selectedHarvestYear;
                }

                var existYear = await dbContext.HarvestYear
                    .OrderBy(f => f.Year)
                    .LastAsync(cancellationToken);
                if (existYear is not null)
                {
                    _selectedHarvestYear = new HarvestYearVm { Id = existYear.Id, Year = existYear.Year };
                    await SetSelectedYearAsync(_selectedHarvestYear);
                    return _selectedHarvestYear;
                }
                else
                {
                    var createdYear = await dbContext.HarvestYear.AddAsync(new HarvestYear { Id = Guid.NewGuid(), Year = DateTime.Now.Year.ToString() }, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    _selectedHarvestYear = new HarvestYearVm { Id = createdYear.Entity.Id, Year = createdYear.Entity.Year };
                    await SetSelectedYearAsync(_selectedHarvestYear);
                    return _selectedHarvestYear;
                }
            }
            return _selectedHarvestYear;
        }
        public async ValueTask SetSelectedYearAsync(HarvestYearVm harvestYear)
        {
            if (_selectedHarvestYear is not null && _selectedHarvestYear.Id == harvestYear.Id)
                return;

            var keyValue = new Parameter
            {
                Key = ParameterKeys.SelectedHarvestYear,
                Value = JsonSerializer.Serialize(harvestYear)
            };

            var dbContextFactory = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IAppDbContextFactory>();
            using var dbContext = dbContextFactory.CreateDbContext();
            var existKey = await dbContext.Parameter.FindAsync([ParameterKeys.SelectedHarvestYear], CancellationToken.None);
            if (existKey is null)
            {
                await dbContext.Parameter.AddAsync(keyValue, CancellationToken.None);
            }
            else
            {
                existKey.Value = keyValue.Value;
                dbContext.Parameter.Update(existKey);
            }
            await dbContext.SaveChangesAsync(CancellationToken.None);

            _selectedHarvestYear = harvestYear;
            OnHarvestYearChanged();
        }

        protected virtual void OnHarvestYearChanged()
        {
            if (_selectedHarvestYear is not null)
                HarvestYearChanged?.Invoke(this, _selectedHarvestYear);
        }

    }
}
