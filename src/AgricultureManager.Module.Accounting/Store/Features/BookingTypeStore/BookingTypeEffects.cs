using AgricultureManager.Module.Accounting.Features.BookingTypeFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Module.Accounting.Store.Features.BookingTypeStore
{
    public class BookingTypeEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadBookingTypesDataAction))]
        public async Task HandleLoadBookingTypesDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetBookingTypeListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadBookingTypesDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadBookingTypeDataResultFailAction());
        }
    }
}
