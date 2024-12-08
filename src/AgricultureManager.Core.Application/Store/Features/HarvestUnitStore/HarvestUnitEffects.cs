using AgricultureManager.Core.Application.Features.HarvestUnitFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.HarvestUnitStore
{
    public class HarvestUnitEffects(IMediator mediator)
    {
        [EffectMethod]
        public async Task HandleLoadHarvestYearsAction(LoadHarvestUnitsDataAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetSelectedHarvestUnitsAction(default!));
            if (action.HarvestYear is null)
                return;

            var response = await mediator.Send(new GetHarvestUnitsFromYear(action.HarvestYear.Id));
            if (response.Success && response.Data is not null)
            {
                dispatcher.Dispatch(new LoadHarvestUnitsDataResultAction(response.Data));
            }
        }
    }
}
