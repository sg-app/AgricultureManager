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
            state with { IsInitialized = true, IsLoading = false, HarvestYears = action.HarvestYears };

        [ReducerMethod]
        public static HarvestYearState ReduceSetSelectedHarvestYearAction(HarvestYearState state, SetSelectedHarvestYearAction action) =>
            state with { SelectedHarvestYear = action.SelectedHarvestYear };

        [ReducerMethod]
        public static HarvestYearState ReduceSaveSelectedHarvestYearAction(HarvestYearState state, SaveSelectedHarvestYearAction action) =>
            state with { SelectedHarvestYear = action.SelectedHarvestYear };

        [ReducerMethod]
        public static HarvestYearState AddHarvestYearReducer(HarvestYearState state, AddHarvestYearAction action)
        {
            var list = state.HarvestYears.ToList();
            var changed = list.Append(action.HarvestYear);
            return state with { HarvestYears = changed.OrderByDescending(f => f.Year) };
        }

        [ReducerMethod]
        public static HarvestYearState RemoveHarvestYearReducer(HarvestYearState state, RemoveHarvestYearAction action)
        {
            var item = state.HarvestYears.First(x => x.Id == action.HarvestYearId);
            var list = state.HarvestYears.ToList();
            list.Remove(item);
            return state with { HarvestYears = list };
        }
    }
}
