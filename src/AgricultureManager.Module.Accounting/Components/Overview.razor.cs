using AgricultureManager.Module.Accounting.Features.AccountMouvementsFeatures;
using AgricultureManager.Module.Accounting.Features.BankingFeatures;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.Features.AccountStore;
using AgricultureManager.Module.Accounting.Store.States;
using AgricultureManager.SharedComponents.Dialogs;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class Overview
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
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

            AccountState.StateChanged += async (s, e) => await OnAccountStateChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            _startDate = DateTime.Now.AddDays(-60).Date;
            _endDate = DateTime.Now.Date;
            await LoadDataAsync();
        }

        private async Task OnAccountStateChanged()
        {
            await LoadDataAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDateChanged(DateTime? date)
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
            if (AccountState.Value.SelectedAccount is null) return;

            _isLoading = true;

            var response = await Mediator.Send(new GetAccountMouvementListCommand(AccountState.Value.SelectedAccount.Id, _startDate, _endDate, _withNoBookings));
            _accountMouvements = response.Data?.OrderByDescending(f => f.InputDate).ToList() ?? [];
            _selectedItem = _accountMouvements.Take(1).ToList();
            _isLoading = false;
        }

        private void OnAccountChanged(AccountVm item)
        {
            Dispatcher.Dispatch(new SetSelectedAccountAction(item));
        }

        private async Task OnLoadClick(MouseEventArgs args)
        {
            var cmd = Mapper.Map<GetMouvementsFromAccountCommand>(AccountState.Value.SelectedAccount);
            cmd.StartDate = _startDate;
            cmd.EndDate = _endDate;
            await RequestPasswordIfMissing(cmd);

            var response = await Mediator.Send(cmd);
            if (response.Success)
                Dispatcher.Dispatch(new LoadAccountsDataAction());
        }

        private async Task RequestPasswordIfMissing(GetMouvementsFromAccountCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.Password))
            {
                var dialogResult = await DialogService.OpenAsync<TextBoxDialog>(
                        "Bitte Passwort eingeben",
                        new() {
                            { nameof(TextBoxDialog.Title), "Passwort eingeben" },
                            { nameof(TextBoxDialog.IsPassword), true }
                        }
                    );

                if (dialogResult is string password)
                    cmd.Password = password;
            }
        }
    }
}
