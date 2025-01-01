using AgricultureManager.Module.Accounting.Models;

namespace AgricultureManager.Module.Accounting.Store.Features.AccountStore
{
    public record LoadAccountsDataAction();
    public record LoadAccountsDataResultAction(IEnumerable<AccountVm> Accounts);
    public record LoadAccountDataResultFailAction();
    public record SetSelectedAccountAction(AccountVm Account);
    public record AddAccountAction(AccountVm Account);
    public record UpdateAccountAction(AccountVm Account);
    public record RemoveAccountAction(Guid AccountId);
}
