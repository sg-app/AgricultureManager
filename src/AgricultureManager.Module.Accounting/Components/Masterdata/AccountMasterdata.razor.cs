using AgricultureManager.Module.Accounting.Features.AccountFeatures;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.Features.AccountStore;
using AgricultureManager.Module.Accounting.Store.States;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.Module.Accounting.Components.Masterdata
{
    public partial class AccountMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<AccountState> AccountState { get; set; } = default!;

        private RadzenDataGrid<AccountVm> _grid = default!;
        private AccountVm? _itemToEditOriginal;
        public string Title => "Konten";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!AccountState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadAccountsDataAction());
        }
        private async Task DeleteRow(AccountVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveAccountCommand(item.Id));
            if (response.Success)
                Dispatcher.Dispatch(new RemoveAccountAction(item.Id));
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(AccountVm item)
        {
            _itemToEditOriginal = Mapper.Map<AccountVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(AccountVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(AccountVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(AccountVm item)
        {
            var cmd = new UpdateAccountCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                Dispatcher.Dispatch(new UpdateAccountAction(response.Data));
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(AccountVm item)
        {
            var cmd = new AddAccountCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                Dispatcher.Dispatch(new AddAccountAction(response.Data));
            }
        }
    }
}
