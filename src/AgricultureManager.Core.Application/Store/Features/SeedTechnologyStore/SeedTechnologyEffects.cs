using AgricultureManager.Core.Application.Features.SeedTechnologyFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.SeedTechnologyStore
{
    public class SeedTechnologyEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadSeedTechnologiesDataAction))]
        public async Task HandleLoadSeedTechnologiesDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetSeedTechnologyListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadSeedTechnologiesDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadSeedTechnologyDataResultFailAction());
        }
    }
}
