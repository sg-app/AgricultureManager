using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record CompanyState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public CompanyVm Company { get; init; } = new();
        private CompanyState() { }

        public CompanyState(bool isLoading, CompanyVm company)
        {
            IsLoading = isLoading;
            Company = company;
        }
    }
}
