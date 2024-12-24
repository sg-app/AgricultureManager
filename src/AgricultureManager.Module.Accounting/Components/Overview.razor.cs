using AgricultureManager.Module.Accounting.Features.AccountMouvementsFeatures;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.Features.AccountStore;
using AgricultureManager.Module.Accounting.Store.States;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class Overview
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<AccountState> AccountState { get; set; } = default!;


        private List<AccountMouvementVm> _accountMouvements = [];
        private DateTime? _startDate;
        private DateTime? _endDate;
        private bool _isLoading = false;
        private bool _withNoBookings = false;
        private IList<AccountMouvementVm>? _selectedItem;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!AccountState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadAccountsDataAction());

            AccountState.StateChanged += async (s, e) => { await LoadDataAsync(); await InvokeAsync(StateHasChanged); };
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _startDate = DateTime.Now.AddDays(-60).Date;
            _endDate = DateTime.Now.Date;
            await LoadDataAsync();
        }

        private async Task StartDateChanged(DateTime? startDate)
        {
            await LoadDataAsync();
        }
        private async Task EndDateChanged(DateTime? startDate)
        {
            await LoadDataAsync();
        }
        private async Task WithNoBookingChanged(bool isChecked)
        {
            _withNoBookings = isChecked;
            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            if (AccountState.Value.SelectedAccount == null && AccountState.Value.Accounts.Any())
                Dispatcher.Dispatch(new SetSelectedAccountAction(AccountState.Value.Accounts.First()));

            if (AccountState.Value.SelectedAccount is null) return;

            _accountMouvements = new();
            _isLoading = true;

            var response = await Mediator.Send(new GetAccountMouvementListCommand(AccountState.Value.SelectedAccount.Id, _startDate, _endDate, _withNoBookings));
            var result = response.Data ?? [];
            _accountMouvements = result.OrderByDescending(f => f.InputDate).ToList();
            _selectedItem = _accountMouvements.Take(1).ToList();
            _isLoading = false;
        }
        private void OnAccountChanged(AccountVm item)
        {
            Dispatcher.Dispatch(new SetSelectedAccountAction(item));
        }

    }
}
