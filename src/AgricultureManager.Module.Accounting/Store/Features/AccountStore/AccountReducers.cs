using AgricultureManager.Module.Accounting.Store.States;
using Fluxor;

namespace AgricultureManager.Module.Accounting.Store.Features.AccountStore
{
    public static class AccountReducers
    {
        [ReducerMethod(typeof(LoadAccountsDataAction))]
        public static AccountState LoadAccountsDataReducer(AccountState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static AccountState LoadAccountsDataResultReducer(AccountState state, LoadAccountsDataResultAction action)
        {
            var selectedAccount = state.SelectedAccount == null && action.Accounts.Any() 
                ? action.Accounts.First() 
                : action.Accounts.FirstOrDefault(f=>f.Id == state.SelectedAccount!.Id);
            return state with { IsInitialized = true, IsLoading = false, Accounts = action.Accounts, SelectedAccount = selectedAccount };
        }

        [ReducerMethod(typeof(LoadAccountDataResultFailAction))]
        public static AccountState LoadAccountDataResultFailReducer(AccountState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static AccountState SetSelectedAccountReducer(AccountState state, SetSelectedAccountAction action)
            => state with { SelectedAccount = action.Account };

        [ReducerMethod]
        public static AccountState AddAccountReducer(AccountState state, AddAccountAction action)
        {
            var list = state.Accounts.ToList();
            var changed = list.Append(action.Account);
            return state with { Accounts = changed };
        }

        [ReducerMethod]
        public static AccountState UpdateAccountReducer(AccountState state, UpdateAccountAction action)
        {
            var item = state.Accounts.First(x => x.Id == action.Account.Id);
            var list = state.Accounts.ToList();
            list.Remove(item);
            var changed = list.Append(action.Account);
            return state with { Accounts = changed };
        }

        [ReducerMethod]
        public static AccountState RemoveAccountReducer(AccountState state, RemoveAccountAction action)
        {
            var item = state.Accounts.First(x => x.Id == action.AccountId);
            var list = state.Accounts.ToList();
            list.Remove(item);
            return state with { Accounts = list };
        }
    }
}
