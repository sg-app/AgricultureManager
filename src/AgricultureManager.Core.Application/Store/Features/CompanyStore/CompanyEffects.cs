using AgricultureManager.Core.Application.Features.CompanyFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.CompanyStore
{
    public class CompanyEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadCompanyDataAction))]
        public async Task HandleLoadCompanysDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetCompanyCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadCompanyDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadCompanyDataResultFailAction());
        }
    }
}
