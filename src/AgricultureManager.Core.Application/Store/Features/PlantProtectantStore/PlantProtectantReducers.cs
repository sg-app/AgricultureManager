using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectantStore
{
    public static class PlantProtectantReducers
    {
        [ReducerMethod(typeof(LoadPlantProtectantsDataAction))]
        public static PlantProtectantState LoadPlantProtectantsDataReducer(PlantProtectantState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static PlantProtectantState LoadPlantProtectantsDataResultReducer(PlantProtectantState state, LoadPlantProtectantsDataResultAction action)
            => state with { IsLoading = false, PlantProtectants = action.PlantProtectants };
    }
}
