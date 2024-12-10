using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.SeedTechnologyStore
{
    public static class SeedTechnologyReducers
    {
        [ReducerMethod(typeof(LoadSeedTechnologiesDataAction))]
        public static SeedTechnologyState LoadSeedTechnologiesDataReducer(SeedTechnologyState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static SeedTechnologyState LoadSeedTechnologiesDataResultReducer(SeedTechnologyState state, LoadSeedTechnologiesDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, SeedTechnologies = action.SeedTechnologies };

        [ReducerMethod(typeof(LoadSeedTechnologyDataResultFailAction))]
        public static SeedTechnologyState LoadSeedTechnologyDataResultFailReducer(SeedTechnologyState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static SeedTechnologyState AddSeedTechnologyReducer(SeedTechnologyState state, AddSeedTechnologyAction action)
        {
            var list = state.SeedTechnologies.ToList();
            var changed = list.Append(action.SeedTechnology);
            return state with { SeedTechnologies = changed };
        }

        [ReducerMethod]
        public static SeedTechnologyState UpdateSeedTechnologyReducer(SeedTechnologyState state, UpdateSeedTechnologyAction action)
        {
            var item = state.SeedTechnologies.First(x => x.Id == action.SeedTechnology.Id);
            var list = state.SeedTechnologies.ToList();
            list.Remove(item);
            var changed = list.Append(action.SeedTechnology);
            return state with { SeedTechnologies = changed };
        }

        [ReducerMethod]
        public static SeedTechnologyState RemoveSeedTechnologyReducer(SeedTechnologyState state, RemoveSeedTechnologyAction action)
        {
            var item = state.SeedTechnologies.First(x => x.Id == action.SeedTechnologyId);
            var list = state.SeedTechnologies.ToList();
            list.Remove(item);
            return state with { SeedTechnologies = list };
        }
    }
}
