using AgricultureManager.Core.Application.Features.FieldFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.FieldStore
{
    public class FieldEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadFieldsDataAction))]
        public async Task HandleLoadFieldsDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetFieldListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadFieldsDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadFieldDataResultFailAction());
        }
    }
}
