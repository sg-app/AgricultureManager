using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.HarvestYearStore
{
    public static class HarvestYearReducers
    {

        [ReducerMethod(typeof(LoadHarvestYearsAction))]
        public static HarvestYearState ReduceLoadHarvestYearsAction(HarvestYearState state) =>
            state with { IsLoading = true };

        [ReducerMethod]
        public static HarvestYearState ReduceLoadHarvestYearsResultAction(HarvestYearState state, LoadHarvestYearsResultAction action) =>
            state with { HarvestYears = action.HarvestYears, IsLoading = false };

        [ReducerMethod]
        public static HarvestYearState ReduceSetSelectedHarvestYearAction(HarvestYearState state, SetSelectedHarvestYearAction action) =>
            state with { SelectedHarvestYear = action.SelectedHarvestYear };

        [ReducerMethod]
        public static HarvestYearState ReduceSaveSelectedHarvestYearAction(HarvestYearState state, SaveSelectedHarvestYearAction action) =>
            state with { SelectedHarvestYear = action.SelectedHarvestYear };

    }
}
