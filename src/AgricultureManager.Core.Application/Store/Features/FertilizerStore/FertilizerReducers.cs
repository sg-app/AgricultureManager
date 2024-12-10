using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerStore
{
    public static class FertilizerReducers
    {
        [ReducerMethod(typeof(LoadFertilizersDataAction))]
        public static FertilizerState LoadFertilizersDataReducer(FertilizerState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static FertilizerState LoadFertilizersDataResultReducer(FertilizerState state, LoadFertilizersDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Fertilizers = action.Fertilizers };

        [ReducerMethod(typeof(LoadFertilizerDataResultFailAction))]
        public static FertilizerState LoadFertilizerDataResultFailReducer(FertilizerState state)
   => state with { IsLoading = false };

        [ReducerMethod]
        public static FertilizerState AddFertilizerReducer(FertilizerState state, AddFertilizerAction action)
        {
            var list = state.Fertilizers.ToList();
            var changed = list.Append(action.Fertilizer);
            return state with { Fertilizers = changed };
        }

        [ReducerMethod]
        public static FertilizerState UpdateFertilizerReducer(FertilizerState state, UpdateFertilizerAction action)
        {
            var item = state.Fertilizers.First(x => x.Id == action.Fertilizer.Id);
            var list = state.Fertilizers.ToList();
            list.Remove(item);
            var changed = list.Append(action.Fertilizer);
            return state with { Fertilizers = changed };
        }

        [ReducerMethod]
        public static FertilizerState RemoveFertilizerReducer(FertilizerState state, RemoveFertilizerAction action)
        {
            var item = state.Fertilizers.First(x => x.Id == action.FertilizerId);
            var list = state.Fertilizers.ToList();
            list.Remove(item);
            return state with { Fertilizers = list };
        }
    }
}
