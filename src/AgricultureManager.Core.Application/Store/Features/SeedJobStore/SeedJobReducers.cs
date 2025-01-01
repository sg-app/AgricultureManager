using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.SeedJobStore
{
    public static class FertilizationJobReducers
    {
        [ReducerMethod]
        public static SeedJobState LoadSeedJobDataReducer(SeedJobState state, LoadSeedJobDataAction action)
            => state with { IsLoading = true, SelectedHarvestUnitId = action.HarvestUnitId };

        [ReducerMethod]
        public static SeedJobState LoadSeedJobDataResultReducer(SeedJobState state, LoadSeedJobDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Seeds = action.Seeds };

        [ReducerMethod(typeof(LoadSeedJobDataResultFailAction))]
        public static SeedJobState LoadSeedJobDataResultFailReducer(SeedJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static SeedJobState AddSeedJobReducer(SeedJobState state, AddSeedJobAction action)
        {
            var list = state.Seeds.ToList();
            var changed = list.Append(action.Seed);
            return state with { Seeds = changed };
        }

        [ReducerMethod]
        public static SeedJobState UpdateSeedJobReducer(SeedJobState state, UpdateSeedJobAction action)
        {
            var itme = state.Seeds.First(x => x.Id == action.Seed.Id);
            var list = state.Seeds.ToList();
            list.Remove(itme);
            var changed = list.Append(action.Seed);
            return state with { Seeds = changed };
        }

        [ReducerMethod]
        public static SeedJobState RemoveSeedJobReducer(SeedJobState state, RemoveSeedJobAction action)
        {
            var itme = state.Seeds.First(x => x.Id == action.SeedId);
            var list = state.Seeds.ToList();
            list.Remove(itme);
            return state with { Seeds = list };
        }
    }
}
