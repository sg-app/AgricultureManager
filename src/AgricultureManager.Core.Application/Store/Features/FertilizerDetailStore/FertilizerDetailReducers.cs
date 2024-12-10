using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerDetailStore
{
    public static class FertilizerDetailReducers
    {
        [ReducerMethod(typeof(LoadFertilizerDetailsDataAction))]
        public static FertilizerDetailState LoadFertilizerDetailsDataReducer(FertilizerDetailState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static FertilizerDetailState LoadFertilizerDetailsDataResultReducer(FertilizerDetailState state, LoadFertilizerDetailsDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, FertilizerDetails = action.FertilizerDetails };

        [ReducerMethod(typeof(LoadFertilizerDetailDataResultFailAction))]
        public static FertilizerDetailState LoadFertilizerDetailDataResultFailReducer(FertilizerDetailState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static FertilizerDetailState AddFertilizerDetailReducer(FertilizerDetailState state, AddFertilizerDetailAction action)
        {
            var list = state.FertilizerDetails.ToList();
            var changed = list.Append(action.FertilizerDetail);
            return state with { FertilizerDetails = changed };
        }

        [ReducerMethod]
        public static FertilizerDetailState UpdateFertilizerDetailReducer(FertilizerDetailState state, UpdateFertilizerDetailAction action)
        {
            var item = state.FertilizerDetails.First(x => x.Id == action.FertilizerDetail.Id);
            var list = state.FertilizerDetails.ToList();
            list.Remove(item);
            var changed = list.Append(action.FertilizerDetail);
            return state with { FertilizerDetails = changed };
        }

        [ReducerMethod]
        public static FertilizerDetailState RemoveFertilizerDetailReducer(FertilizerDetailState state, RemoveFertilizerDetailAction action)
        {
            var item = state.FertilizerDetails.First(x => x.Id == action.FertilizerDetailId);
            var list = state.FertilizerDetails.ToList();
            list.Remove(item);
            return state with { FertilizerDetails = list };
        }
    }
}
