using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record HarvestYearState
    {
        public bool IsLoading { get; init; }
        public HarvestYearVm? SelectedHarvestYear { get; init; }
        public IEnumerable<HarvestYearVm>? HarvestYears { get; init; }
        private HarvestYearState() { }
        public HarvestYearState(bool isLoading, HarvestYearVm selectedHarvestYear, IEnumerable<HarvestYearVm> harvestYears)
        {
            IsLoading = isLoading;
            SelectedHarvestYear = selectedHarvestYear;
            HarvestYears = harvestYears;
        }
    }
}
