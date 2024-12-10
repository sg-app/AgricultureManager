using AgricultureManager.Core.Application.Features.CultureFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Store.Features.CultureStore;
using AgricultureManager.Core.Application.Store.States;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class CultureMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<CultureState> CultureState { get; set; } = default!;

        private RadzenDataGrid<CultureVm> _grid = default!;
        private CultureVm? _itemToEditOriginal;
        public string Title => "Kultur";

        private async Task DeleteRow(CultureVm item)
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

        private async Task EditRow(CultureVm item)
        {
            _itemToEditOriginal = Mapper.Map<CultureVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(CultureVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(CultureVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(CultureVm item)
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
        private async Task OnCreateRow(CultureVm item)
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
