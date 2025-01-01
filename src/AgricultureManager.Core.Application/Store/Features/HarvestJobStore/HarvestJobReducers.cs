using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.HarvestJobStore
{
    public static class HarvestJobReducers
    {
        [ReducerMethod]
        public static HarvestJobState LoadHarvestJobDataReducer(HarvestJobState state, LoadHarvestJobDataAction action)
            => state with { IsLoading = true, SelectedHarvestUnitId = action.HarvestUnitId };

        [ReducerMethod]
        public static HarvestJobState LoadHarvestJobDataResultReducer(HarvestJobState state, LoadHarvestJobDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Harvests = action.Harvests };

        [ReducerMethod(typeof(LoadHarvestJobDataResultFailAction))]
        public static HarvestJobState LoadHarvestJobDataResultFailReducer(HarvestJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static HarvestJobState AddHarvestJobReducer(HarvestJobState state, AddHarvestJobAction action)
        {
            var list = state.Harvests.ToList();
            var changed = list.Append(action.Harvest);
            return state with { Harvests = changed };
        }

        [ReducerMethod]
        public static HarvestJobState UpdateHarvestJobReducer(HarvestJobState state, UpdateHarvestJobAction action)
        {
            var item = state.Harvests.First(x => x.Id == action.Harvest.Id);
            var list = state.Harvests.ToList();
            list.Remove(item);
            var changed = list.Append(action.Harvest);
            return state with { Harvests = changed };
        }

        [ReducerMethod]
        public static HarvestJobState RemoveHarvestJobReducer(HarvestJobState state, RemoveHarvestJobAction action)
        {
            var item = state.Harvests.First(x => x.Id == action.HarvestId);
            var list = state.Harvests.ToList();
            list.Remove(item);
            return state with { Harvests = list };
        }
    }
}
