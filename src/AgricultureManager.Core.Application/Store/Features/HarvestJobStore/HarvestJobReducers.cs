using AgricultureManager.Core.Application.Store.States;
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
            => state with { IsLoading = false, Harvests = action.Harvests };

        [ReducerMethod(typeof(LoadHarvestJobDataResultFailAction))]
        public static HarvestJobState LoadHarvestJobDataResultFailReducer(HarvestJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static HarvestJobState AddHarvestJobReducer(HarvestJobState state, AddHarvestJobAction action)
        {
            var seeds = state.Harvests.ToList();
            var changed = seeds.Append(action.Harvest);
            return state with { Harvests = changed };
        }

        [ReducerMethod]
        public static HarvestJobState UpdateHarvestJobReducer(HarvestJobState state, UpdateHarvestJobAction action)
        {
            var seed = state.Harvests.First(x => x.Id == action.Harvest.Id);
            var seeds = state.Harvests.ToList();
            seeds.Remove(seed);
            var changed = seeds.Append(action.Harvest);
            return state with { Harvests = changed };
        }

        [ReducerMethod]
        public static HarvestJobState RemoveHarvestJobReducer(HarvestJobState state, RemoveHarvestJobAction action)
        {
            var seed = state.Harvests.First(x => x.Id == action.HarvestId);
            var seeds = state.Harvests.ToList();
            seeds.Remove(seed);
            return state with { Harvests = seeds };
        }
    }
}
