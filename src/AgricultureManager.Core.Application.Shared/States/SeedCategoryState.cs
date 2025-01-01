using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record SeedCategoryState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<SeedCategoryVm> SeedCategories { get; init; } = [];
        private SeedCategoryState() { }

        public SeedCategoryState(bool isLoading, IEnumerable<SeedCategoryVm> seedCategories)
        {
            IsLoading = isLoading;
            SeedCategories = seedCategories;
        }
    }
}
