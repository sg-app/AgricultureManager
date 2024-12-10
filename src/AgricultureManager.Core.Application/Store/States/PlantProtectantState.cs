using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record PlantProtectantState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<PlantProtectantVm> PlantProtectants { get; init; } = [];

        private PlantProtectantState() { }

        public PlantProtectantState(bool isLoading, bool isInitialized, IEnumerable<PlantProtectantVm> plantProtectant)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            PlantProtectants = plantProtectant;
        }
    }
}
