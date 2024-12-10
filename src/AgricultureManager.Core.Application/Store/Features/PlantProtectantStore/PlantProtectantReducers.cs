using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectantStore
{
    public static class PlantProtectantReducers
    {
        [ReducerMethod(typeof(LoadPlantProtectantsDataAction))]
        public static PlantProtectantState LoadPlantProtectantsDataReducer(PlantProtectantState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static PlantProtectantState LoadPlantProtectantsDataResultReducer(PlantProtectantState state, LoadPlantProtectantsDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, PlantProtectants = action.PlantProtectants };

        [ReducerMethod(typeof(LoadPlantProtectantDataResultFailAction))]
        public static PlantProtectantState LoadPlantProtectantDataResultFailReducer(PlantProtectantState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static PlantProtectantState AddPlantProtectantReducer(PlantProtectantState state, AddPlantProtectantAction action)
        {
            var list = state.PlantProtectants.ToList();
            var changed = list.Append(action.PlantProtectant);
            return state with { PlantProtectants = changed };
        }

        [ReducerMethod]
        public static PlantProtectantState UpdatePlantProtectantReducer(PlantProtectantState state, UpdatePlantProtectantAction action)
        {
            var item = state.PlantProtectants.First(x => x.Id == action.PlantProtectant.Id);
            var list = state.PlantProtectants.ToList();
            list.Remove(item);
            var changed = list.Append(action.PlantProtectant);
            return state with { PlantProtectants = changed };
        }

        [ReducerMethod]
        public static PlantProtectantState RemovePlantProtectantReducer(PlantProtectantState state, RemovePlantProtectantAction action)
        {
            var item = state.PlantProtectants.First(x => x.Id == action.PlantProtectantId);
            var list = state.PlantProtectants.ToList();
            list.Remove(item);
            return state with { PlantProtectants = list };
        }
    }
}
