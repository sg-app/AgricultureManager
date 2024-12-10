using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record PlantProtectionJobState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public Guid SelectedHarvestUnitId { get; init; }
        public IEnumerable<PlantProtectionVm> PlantProtections { get; init; } = [];
        private PlantProtectionJobState() { }
        public PlantProtectionJobState(bool isLoading, bool isInitialized, Guid selectedHarvestUnitId, IEnumerable<PlantProtectionVm> seeds)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            SelectedHarvestUnitId = selectedHarvestUnitId;
            PlantProtections = seeds;
        }
    }
}
