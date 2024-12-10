using AgricultureManager.Core.Application.Features.FertilizerFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerStore
{
    public class FertilizerEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadFertilizersDataAction))]
        public async Task HandleLoadFertilizersDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetFertilizerListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadFertilizersDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadFertilizerDataResultFailAction());
        }
    }
}
