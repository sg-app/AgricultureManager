using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record HarvestYearState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public HarvestYearVm? SelectedHarvestYear { get; init; }
        public IEnumerable<HarvestYearVm> HarvestYears { get; init; } = [];
        public IEnumerable<HarvestYearVm> HarvestYearsDropdown { get; init; } = [];
        private HarvestYearState() { }
        public HarvestYearState(bool isLoading, HarvestYearVm selectedHarvestYear, IEnumerable<HarvestYearVm> harvestYears)
        {
            IsLoading = isLoading;
            SelectedHarvestYear = selectedHarvestYear;
            HarvestYears = harvestYears;
        }
    }
}
