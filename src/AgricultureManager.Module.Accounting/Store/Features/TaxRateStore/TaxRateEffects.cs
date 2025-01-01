using AgricultureManager.Module.Accounting.Features.TaxRateFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Module.Accounting.Store.Features.TaxRateStore
{
    public class TaxRateEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadTaxRatesDataAction))]
        public async Task HandleLoadTaxRatesDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetTaxRateListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadTaxRatesDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadTaxRateDataResultFailAction());
        }
    }
}
