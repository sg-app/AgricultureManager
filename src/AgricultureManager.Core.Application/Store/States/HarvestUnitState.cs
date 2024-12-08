using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record HarvestUnitState
    {
        public bool IsLoading { get; init; }
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
