using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Module.Accounting.Models;
using Fluxor;

namespace AgricultureManager.Module.Accounting.Store.States
{
    [FeatureState]
    public record TaxRateState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<TaxRateVm> TaxRates { get; init; } = [];
        private TaxRateState() { }
        public TaxRateState(bool isLoading, bool isInitialized, IEnumerable<TaxRateVm> taxRates)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            TaxRates = taxRates;
        }
    }
}
