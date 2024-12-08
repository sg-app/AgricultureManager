using AgricultureManager.Core.Application.Store.States;
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
            => state with { IsLoading = false, SeedCategories = action.SeedCategories };
    }
}
