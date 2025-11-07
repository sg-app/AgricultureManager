using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record HarvestYearState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public HarvestYearVm? SelectedHarvestYear { get; init; }
        private HarvestYearState() { }
        public HarvestYearState(bool isLoading, HarvestYearVm selectedHarvestYear)
        {
            IsLoading = isLoading;
            SelectedHarvestYear = selectedHarvestYear;
        }
    }
}
