using AgricultureManager.Core.Application.Features.PlantProtectionFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectionJobStore
{
    public class PlantProtectionEffects(IMediator mediator)
    {
        [EffectMethod]
        public async Task HandleLoadPlantProtectionJobDataAction(LoadPlantProtectionJobDataAction action, IDispatcher dispatcher)
        {
            var response = await mediator.Send(new GetPlantProtectionListCommand(action.HarvestUnitId));
            if (response.Success && response.Data is not null)
                dispatcher.Dispatch(new LoadPlantProtectionJobDataResultAction(response.Data));
            else
                dispatcher.Dispatch(new LoadPlantProtectionJobDataResultFailAction());
        }
    }
}
