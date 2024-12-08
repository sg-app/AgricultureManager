using AgricultureManager.Core.Application.Features.CultureFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.CultureStore
{
    public class CultureEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadCulturesDataAction))]
        public async Task HandleLoadCulturesDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetCultureListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadCulturesDataResultAction(respose.Data));

        }
    }
}
