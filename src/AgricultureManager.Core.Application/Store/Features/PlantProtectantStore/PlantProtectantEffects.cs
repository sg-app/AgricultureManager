using AgricultureManager.Core.Application.Features.PlantProtectantFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectantStore
{
    public class PlantProtectantEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadPlantProtectantsDataAction))]
        public async Task HandleLoadPlantProtectantsDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetPlantProtectantListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadPlantProtectantsDataResultAction(respose.Data));

        }
    }
}
