using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record SeedCategoryState
    {
        public bool IsLoading { get; init; }
        public IEnumerable<SeedCategoryVm>? SeedCategories { get; init; }
        private SeedCategoryState() { }

        public SeedCategoryState(bool isLoading, IEnumerable<SeedCategoryVm>? seedCategories)
        {
            IsLoading = isLoading;
            SeedCategories = seedCategories;
        }
    }
}
