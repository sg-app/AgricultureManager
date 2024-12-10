using AgricultureManager.Core.Application.Features.PersonFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.PeopleStore
{
    public class PeopleEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadPeoplesDataAction))]
        public async Task HandleLoadPeoplesDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetPersonListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadPeoplesDataResultAction(respose.Data));
            else
                dispatcher.Dispatch(new LoadPeopleDataResultFailAction());
        }
    }
}
