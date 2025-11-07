using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.CoreApp.Store.Features.HarvestYearStore
{
    public static class HarvestYearReducers
    {

        [ReducerMethod]
        public static HarvestYearState ReduceSetSelectedHarvestYearAction(HarvestYearState state, SetSelectedHarvestYearAction action) =>
            state with { SelectedHarvestYear = action.SelectedHarvestYear };

        [ReducerMethod]
        public static HarvestYearState ReduceSaveSelectedHarvestYearAction(HarvestYearState state, SaveSelectedHarvestYearAction action) =>
            state with { SelectedHarvestYear = action.SelectedHarvestYear };

    }
}
