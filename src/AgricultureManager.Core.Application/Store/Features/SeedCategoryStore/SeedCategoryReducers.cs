using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.SeedCategoryStore
{
    public static class SeedCategoryReducers
    {
        [ReducerMethod(typeof(LoadSeedCategoriesDataAction))]
        public static SeedCategoryState LoadSeedCategoriesDataReducer(SeedCategoryState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static SeedCategoryState LoadSeedCategoriesDataResultReducer(SeedCategoryState state, LoadSeedCategoriesDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, SeedCategories = action.SeedCategories };

        [ReducerMethod(typeof(LoadSeedCategoryDataResultFailAction))]
        public static SeedCategoryState LoadSeedCategoryDataResultFailReducer(SeedCategoryState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static SeedCategoryState AddSeedCategoryReducer(SeedCategoryState state, AddSeedCategoryAction action)
        {
            var list = state.SeedCategories.ToList();
            var changed = list.Append(action.SeedCategory);
            return state with { SeedCategories = changed };
        }

        [ReducerMethod]
        public static SeedCategoryState UpdateSeedCategoryReducer(SeedCategoryState state, UpdateSeedCategoryAction action)
        {
            var item = state.SeedCategories.First(x => x.Id == action.SeedCategory.Id);
            var list = state.SeedCategories.ToList();
            list.Remove(item);
            var changed = list.Append(action.SeedCategory);
            return state with { SeedCategories = changed };
        }

        [ReducerMethod]
        public static SeedCategoryState RemoveSeedCategoryReducer(SeedCategoryState state, RemoveSeedCategoryAction action)
        {
            var item = state.SeedCategories.First(x => x.Id == action.SeedCategoryId);
            var list = state.SeedCategories.ToList();
            list.Remove(item);
            return state with { SeedCategories = list };
        }
    }
}
