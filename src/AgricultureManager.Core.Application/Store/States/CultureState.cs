using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record CultureState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<CultureVm> Cultures { get; init; } = [];
        private CultureState() { }

        public CultureState(bool isLoading, IEnumerable<CultureVm> cultures)
        {
            IsLoading = isLoading;
            Cultures = cultures;
        }
    }
}
