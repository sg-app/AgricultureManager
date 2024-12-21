using AgricultureManager.Core.Application.Features.CultureFeatures;
using AgricultureManager.Core.Application.Store.Features.CultureStore;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.Features.TaxRateStore;
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
    public partial class TaxRateMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<TaxRateState> TaxRateState { get; set; } = default!;

        private RadzenDataGrid<TaxRateVm> _grid = default!;
        private TaxRateVm? _itemToEditOriginal;
        public string Title => "Steuersatz";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!TaxRateState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadTaxRatesDataAction());
        }
        private async Task DeleteRow(TaxRateVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveCultureCommand { Id = item.Id });
            if (response.Success)
                Dispatcher.Dispatch(new RemoveCultureAction(item.Id));
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(TaxRateVm item)
        {
            _itemToEditOriginal = Mapper.Map<TaxRateVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(TaxRateVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(TaxRateVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(TaxRateVm item)
        {
            var cmd = new UpdateCultureCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                Dispatcher.Dispatch(new UpdateCultureAction(response.Data));
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(TaxRateVm item)
        {
            var cmd = new AddCultureCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                Dispatcher.Dispatch(new AddCultureAction(response.Data));
            }
        }
    }
}
