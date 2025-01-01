using AgricultureManager.Core.Application.Features.FertilizerDetailFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerDetailStore
{
    public class FertilizerDetailEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadFertilizerDetailsDataAction))]
        public async Task HandleLoadFertilizerDetailsDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetFertilizerDetailListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadFertilizerDetailsDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadFertilizerDetailDataResultFailAction());
        }
    }
}
