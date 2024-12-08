using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerStore
{
    public static class FertilizerReducers
    {
        [ReducerMethod(typeof(LoadFertilizersDataAction))]
        public static FertilizerState LoadFertilizersDataReducer(FertilizerState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static FertilizerState LoadFertilizersDataResultReducer(FertilizerState state, LoadFertilizersDataResultAction action)
            => state with { IsLoading = false, Fertilizers = action.Fertilizers };
    }
}
