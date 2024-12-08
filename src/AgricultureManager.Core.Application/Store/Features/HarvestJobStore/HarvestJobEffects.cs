using AgricultureManager.Core.Application.Features.HarvestFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.HarvestJobStore
{
    public class HarvestJobEffects(IMediator mediator)
    {
        [EffectMethod]
        public async Task HandleLoadHarvestJobDataAction(LoadHarvestJobDataAction action, IDispatcher dispatcher)
        {
            var response = await mediator.Send(new GetHarvestListCommand(action.HarvestUnitId));
            if (response.Success && response.Data is not null)
                dispatcher.Dispatch(new LoadHarvestJobDataResultAction(response.Data));
            else
                dispatcher.Dispatch(new LoadHarvestJobDataResultFailAction());
        }
    }
}
