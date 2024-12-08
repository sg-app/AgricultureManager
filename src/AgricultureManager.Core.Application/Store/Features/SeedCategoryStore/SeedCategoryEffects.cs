using AgricultureManager.Core.Application.Features.SeedCategoryFeatures;
using Fluxor;
using MediatR;

namespace AgricultureManager.Core.Application.Store.Features.SeedCategoryStore
{
    public class SeedCategoryEffects(IMediator mediator)
    {
        [EffectMethod(typeof(LoadSeedCategoriesDataAction))]
        public async Task HandleLoadSeedCategoriesDataAction(IDispatcher dispatcher)
        {
            var respose = await mediator.Send(new GetSeedCategoryListCommand());

            if (respose.Success && respose.Data is not null)
                dispatcher.Dispatch(new LoadSeedCategoriesDataResultAction(respose.Data));

        }
    }
}
