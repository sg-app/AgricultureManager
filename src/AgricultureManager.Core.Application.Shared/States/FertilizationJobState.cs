using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record FertilizationJobState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public Guid SelectedHarvestUnitId { get; init; }
        public IEnumerable<FertilizationVm> Fertilizations { get; init; } = [];
        private FertilizationJobState() { }
        public FertilizationJobState(bool isLoading, bool isInitialized, Guid selectedHarvestUnitId, IEnumerable<FertilizationVm> seeds)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            SelectedHarvestUnitId = selectedHarvestUnitId;
            Fertilizations = seeds;
        }
    }
}
