using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record HarvestJobState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public Guid SelectedHarvestUnitId { get; init; }
        public IEnumerable<HarvestVm> Harvests { get; init; } = [];
        private HarvestJobState() { }
        public HarvestJobState(bool isLoading, bool isInitialized, Guid selectedHarvestUnitId, IEnumerable<HarvestVm> seeds)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            SelectedHarvestUnitId = selectedHarvestUnitId;
            Harvests = seeds;
        }
    }
}
