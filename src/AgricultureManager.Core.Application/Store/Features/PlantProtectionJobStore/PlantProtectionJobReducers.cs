using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectionJobStore
{
    public static class FertilizationJobReducers
    {
        [ReducerMethod]
        public static PlantProtectionJobState LoadPlantProtectionJobDataReducer(PlantProtectionJobState state, LoadPlantProtectionJobDataAction action)
            => state with { IsLoading = true, SelectedHarvestUnitId = action.HarvestUnitId };

        [ReducerMethod]
        public static PlantProtectionJobState LoadPlantProtectionJobDataResultReducer(PlantProtectionJobState state, LoadPlantProtectionJobDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, PlantProtections = action.PlantProtections };

        [ReducerMethod(typeof(LoadPlantProtectionJobDataResultFailAction))]
        public static PlantProtectionJobState LoadPlantProtectionJobDataResultFailReducer(PlantProtectionJobState state)
            => state with { IsLoading = false };

        [ReducerMethod]
        public static PlantProtectionJobState AddPlantProtectionJobReducer(PlantProtectionJobState state, AddPlantProtectionJobAction action)
        {
            var list = state.PlantProtections.ToList();
            var changed = list.Append(action.PlantProtection);
            return state with { PlantProtections = changed };
        }

        [ReducerMethod]
        public static PlantProtectionJobState UpdatePlantProtectionJobReducer(PlantProtectionJobState state, UpdatePlantProtectionJobAction action)
        {
            var item = state.PlantProtections.First(x => x.Id == action.PlantProtection.Id);
            var list = state.PlantProtections.ToList();
            list.Remove(item);
            var changed = list.Append(action.PlantProtection);
            return state with { PlantProtections = changed };
        }

        [ReducerMethod]
        public static PlantProtectionJobState RemovePlantProtectionJobReducer(PlantProtectionJobState state, RemovePlantProtectionJobAction action)
        {
            var item = state.PlantProtections.First(x => x.Id == action.PlantProtectionId);
            var list = state.PlantProtections.ToList();
            list.Remove(item);
            return state with { PlantProtections = list };
        }
    }
}
