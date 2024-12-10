using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record FertilizerState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<FertilizerVm> Fertilizers { get; init; } = [];

        private FertilizerState() { }

        public FertilizerState(bool isLoading, bool isInitialized, IEnumerable<FertilizerVm> fertilizers)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            Fertilizers = fertilizers;
        }
    }
}
