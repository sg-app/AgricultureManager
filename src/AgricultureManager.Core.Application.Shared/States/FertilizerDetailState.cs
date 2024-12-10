using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record FertilizerDetailState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<FertilizerDetailVm> FertilizerDetails { get; init; } = [];
        private FertilizerDetailState() { }

        public FertilizerDetailState(bool isLoading, IEnumerable<FertilizerDetailVm> fertilizerDetails)
        {
            IsLoading = isLoading;
            FertilizerDetails = fertilizerDetails;
        }
    }
}
