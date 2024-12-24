using AgricultureManager.Module.Accounting.Features.AccountFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Module.Accounting.Store.Features.AccountStore
{
    public class AccountEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadAccountsDataAction))]
        public async Task HandleLoadAccountsDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetAccountListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadAccountsDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadAccountDataResultFailAction());
        }
    }
}
