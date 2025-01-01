using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.HarvestUnitStore
{
    public static class HarvestUnitReducers
    {
        [ReducerMethod]
        public static HarvestUnitState SetSelectedHarvestUnitsReducer(HarvestUnitState state, SetSelectedHarvestUnitsAction action)
            => state with { SelectedHarvestUnits = action.HarvestUnitsOverview };

        [ReducerMethod(typeof(LoadHarvestUnitsDataAction))]
        public static HarvestUnitState LoadHarvestUnitsDataReducer(HarvestUnitState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static HarvestUnitState LoadHarvestUnitsDataResultReducer(HarvestUnitState state, LoadHarvestUnitsDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, HarvestUnitsOverview = action.HarvestUnitsOverview };
    }
}
