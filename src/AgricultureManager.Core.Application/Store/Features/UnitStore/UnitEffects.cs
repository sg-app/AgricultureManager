using AgricultureManager.Core.Application.Features.UnitFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.UnitStore
{
    public class UnitEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadUnitsDataAction))]
        public async Task HandleLoadUnitsDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetUnitListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadUnitsDataResultAction(respose.Data));

        }
    }
}
