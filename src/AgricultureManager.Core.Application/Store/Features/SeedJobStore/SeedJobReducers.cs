using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.SeedJobStore
{
    public static class SeedJobReducers
    {
        [ReducerMethod]
        public static SeedJobState LoadSeedJobDataReducer(SeedJobState state, LoadSeedJobDataAction action)
            => state with { IsLoading = true, SelectedHarvestUnitId = action.HarvestUnitId };

        [ReducerMethod]
        public static SeedJobState LoadSeedJobDataResultReducer(SeedJobState state, LoadSeedJobDataResultAction action)
            => state with { IsLoading = false, Seeds = action.Seeds };

        [ReducerMethod(typeof(LoadSeedJobDataResultFailAction))]
        public static SeedJobState LoadSeedJobDataResultFailReducer(SeedJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static SeedJobState AddSeedJobReducer(SeedJobState state, AddSeedJobAction action)
        {
            var seeds = state.Seeds.ToList();
            var changed = seeds.Append(action.Seed);
            return state with { Seeds = changed };
        }

        [ReducerMethod]
        public static SeedJobState UpdateSeedJobReducer(SeedJobState state, UpdateSeedJobAction action)
        {
            var seed = state.Seeds.First(x => x.Id == action.Seed.Id);
            var seeds = state.Seeds.ToList();
            seeds.Remove(seed);
            var changed = seeds.Append(action.Seed);
            return state with { Seeds = changed };
        }

        [ReducerMethod]
        public static SeedJobState RemoveSeedJobReducer(SeedJobState state, RemoveSeedJobAction action)
        {
            var seed = state.Seeds.First(x => x.Id == action.SeedId);
            var seeds = state.Seeds.ToList();
            seeds.Remove(seed);
            return state with { Seeds = seeds };
        }
    }
}
