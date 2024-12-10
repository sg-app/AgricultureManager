using AgricultureManager.Core.Application.Shared.States;
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
            => state with { IsInitialized = true, IsLoading = false, Fertilizations = action.Fertilizations };

        [ReducerMethod(typeof(LoadFertilizationJobDataResultFailAction))]
        public static FertilizationJobState LoadFertilizationJobDataResultFailReducer(FertilizationJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static FertilizationJobState AddFertilizationJobReducer(FertilizationJobState state, AddFertilizationJobAction action)
        {
            var list = state.Fertilizations.ToList();
            var changed = list.Append(action.Fertilization);
            return state with { Fertilizations = changed };
        }

        [ReducerMethod]
        public static FertilizationJobState UpdateFertilizationJobReducer(FertilizationJobState state, UpdateFertilizationJobAction action)
        {
            var item = state.Fertilizations.First(x => x.Id == action.Fertilization.Id);
            var list = state.Fertilizations.ToList();
            list.Remove(item);
            var changed = list.Append(action.Fertilization);
            return state with { Fertilizations = changed };
        }

        [ReducerMethod]
        public static FertilizationJobState RemoveFertilizationJobReducer(FertilizationJobState state, RemoveFertilizationJobAction action)
        {
            var item = state.Fertilizations.First(x => x.Id == action.FertilizationId);
            var list = state.Fertilizations.ToList();
            list.Remove(item);
            return state with { Fertilizations = list };
        }
    }
}
