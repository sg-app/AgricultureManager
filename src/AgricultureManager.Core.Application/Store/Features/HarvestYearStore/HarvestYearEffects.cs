using AgricultureManager.Core.Application.Common;
using AgricultureManager.Core.Application.Features.HarvestYearFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using Fluxor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;


namespace AgricultureManager.Core.Application.Store.Features.HarvestYearStore
{
    public class HarvestYearEffects(IServiceProvider serviceProvider, IMediator mediator)
    {

        [EffectMethod(typeof(LoadHarvestYearsAction))]
        public async Task HandleLoadHarvestYearsAction(IDispatcher dispatcher)
        {
            var response = await mediator.Send(new GetHarvestYearsCommand());
            if (response.Success && response.Data is not null)
                dispatcher.Dispatch(new LoadHarvestYearsResultAction(response.Data));
        }

        [EffectMethod(typeof(GetCurrentHarvestYearAction))]
        public async Task HandleGetCurrentHarvestYearAction(IDispatcher dispatcher)
        {

            var dbContextFactory = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IAppDbContextFactory>();
            using var dbContext = dbContextFactory.CreateDbContext();
            var cancellationToken = CancellationToken.None;
            var param = await dbContext.Parameter.FindAsync([ParameterKeys.SelectedHarvestYear], cancellationToken);
            HarvestYearVm? harvestYear = null;
            if (param is not null)
            {
                harvestYear = JsonSerializer.Deserialize<HarvestYearVm>(param.Value);
                if (harvestYear is not null)
                {
                    var harvestYearEntity = await dbContext.HarvestYear.FindAsync([harvestYear.Id], cancellationToken);
                    if (harvestYearEntity is not null)
                    {
                        dispatcher.Dispatch(new SetSelectedHarvestYearAction(harvestYear));
                        return;
                    }
                }
            }

            var existYear = await dbContext.HarvestYear
                .OrderBy(f => f.Year)
                .LastAsync(cancellationToken);
            if (existYear is not null)
            {
                harvestYear = new HarvestYearVm { Id = existYear.Id, Year = existYear.Year };
                dispatcher.Dispatch(new SaveSelectedHarvestYearAction(harvestYear));
                return;
            }
            else
            {
                var createdYear = await dbContext.HarvestYear.AddAsync(new HarvestYear { Id = Guid.NewGuid(), Year = DateTime.Now.Year.ToString() }, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                harvestYear = new HarvestYearVm { Id = createdYear.Entity.Id, Year = createdYear.Entity.Year };
                dispatcher.Dispatch(new SaveSelectedHarvestYearAction(harvestYear));
                return;
            }
        }

        [EffectMethod]
        public async Task HandleSaveSelectedHarvestYearAction(SaveSelectedHarvestYearAction action, IDispatcher _)
        {

            var keyValue = new Parameter
            {
                Key = ParameterKeys.SelectedHarvestYear,
                Value = JsonSerializer.Serialize(action.SelectedHarvestYear)
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
        }

    }
}
