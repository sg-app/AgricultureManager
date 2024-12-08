using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.FertilizationJobStore
{
    public static class FertilizationJobReducers
    {
        [ReducerMethod]
        public static FertilizationJobState LoadFertilizationJobDataReducer(FertilizationJobState state, LoadFertilizationJobDataAction action)
            => state with { IsLoading = true, SelectedHarvestUnitId = action.HarvestUnitId };

        [ReducerMethod]
        public static FertilizationJobState LoadFertilizationJobDataResultReducer(FertilizationJobState state, LoadFertilizationJobDataResultAction action)
            => state with { IsLoading = false, Fertilizations = action.Fertilizations };

        [ReducerMethod(typeof(LoadFertilizationJobDataResultFailAction))]
        public static FertilizationJobState LoadFertilizationJobDataResultFailReducer(FertilizationJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static FertilizationJobState AddFertilizationJobReducer(FertilizationJobState state, AddFertilizationJobAction action)
        {
            var seeds = state.Fertilizations.ToList();
            var changed = seeds.Append(action.Fertilization);
            return state with { Fertilizations = changed };
        }

        [ReducerMethod]
        public static FertilizationJobState UpdateFertilizationJobReducer(FertilizationJobState state, UpdateFertilizationJobAction action)
        {
            var seed = state.Fertilizations.First(x => x.Id == action.Fertilization.Id);
            var seeds = state.Fertilizations.ToList();
            seeds.Remove(seed);
            var changed = seeds.Append(action.Fertilization);
            return state with { Fertilizations = changed };
        }

        [ReducerMethod]
        public static FertilizationJobState RemoveFertilizationJobReducer(FertilizationJobState state, RemoveFertilizationJobAction action)
        {
            var seed = state.Fertilizations.First(x => x.Id == action.FertilizationId);
            var seeds = state.Fertilizations.ToList();
            seeds.Remove(seed);
            return state with { Fertilizations = seeds };
        }
    }
}
