using AgricultureManager.Core.Application.Features.FertilizationFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.FertilizationJobStore
{
    public class FertilizationJobEffects(IMediator mediator)
    {
        [EffectMethod]
        public async Task HandleLoadFertilizationJobDataAction(LoadFertilizationJobDataAction action, IDispatcher dispatcher)
        {
            var response = await mediator.Send(new GetFertilizationListCommand(action.HarvestUnitId));
            if (response.Success && response.Data is not null)
                dispatcher.Dispatch(new LoadFertilizationJobDataResultAction(response.Data));
            else
                dispatcher.Dispatch(new LoadFertilizationJobDataResultFailAction());
        }
    }
}
