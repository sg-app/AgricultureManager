using AgricultureManager.Core.Application.Features.SeedFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.SeedJobStore
{
    public class SeedJobEffects(IMediator mediator)
    {
        [EffectMethod]
        public async Task HandleLoadSeedJobDataAction(LoadSeedJobDataAction action, IDispatcher dispatcher)
        {
            var response = await mediator.Send(new GetSeedListCommand(action.HarvestUnitId));
            if (response.Success && response.Data is not null)
                dispatcher.Dispatch(new LoadSeedJobDataResultAction(response.Data));
            else
                dispatcher.Dispatch(new LoadSeedJobDataResultFailAction());
        }
    }
}
