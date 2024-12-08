using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record SeedJobState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public Guid SelectedHarvestUnitId { get; init; }
        public IEnumerable<SeedVm> Seeds { get; init; } = [];
        private SeedJobState() { }
        public SeedJobState(bool isLoading, bool isInitialized, Guid selectedHarvestUnitId, IEnumerable<SeedVm> seeds)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            SelectedHarvestUnitId = selectedHarvestUnitId;
            Seeds = seeds;
        }
    }
}
