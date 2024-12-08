using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.SeedTechnologyStore
{
    public static class SeedTechnologyReducers
    {
        [ReducerMethod(typeof(LoadSeedTechnologiesDataAction))]
        public static SeedTechnologyState LoadSeedTechnologiesDataReducer(SeedTechnologyState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static SeedTechnologyState LoadSeedTechnologiesDataResultReducer(SeedTechnologyState state, LoadSeedTechnologiesDataResultAction action)
            => state with { IsLoading = false, SeedTechnologies = action.SeedTechnologies };
    }
}
