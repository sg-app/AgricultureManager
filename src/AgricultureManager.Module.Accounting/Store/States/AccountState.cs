using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Module.Accounting.Models;
using Fluxor;

namespace AgricultureManager.Module.Accounting.Store.States
{
    [FeatureState]
    public record AccountState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<AccountVm> Accounts { get; init; } = [];
        public AccountVm? SelectedAccount { get; init; }
        private AccountState() { }
        public AccountState(bool isLoading, bool isInitialized, IEnumerable<AccountVm> accounts, AccountVm selectedAccount)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            Accounts = accounts;
            SelectedAccount = selectedAccount;
        }
    }
}
