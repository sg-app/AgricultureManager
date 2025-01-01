using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record HarvestUnitState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<HarvestUnitOverview> HarvestUnitsOverview { get; init; } = [];
        public IList<HarvestUnitOverview> SelectedHarvestUnits { get; init; } = [];
        private HarvestUnitState() { }

        public HarvestUnitState(bool isLoading, IEnumerable<HarvestUnitOverview> harvestUnitsOverview, IList<HarvestUnitOverview> selectedHarvestUnits)
        {
            IsLoading = isLoading;
            HarvestUnitsOverview = harvestUnitsOverview;
            SelectedHarvestUnits = selectedHarvestUnits;
        }
    }
}
