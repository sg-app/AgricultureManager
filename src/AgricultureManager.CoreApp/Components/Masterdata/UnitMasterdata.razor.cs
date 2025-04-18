﻿using AgricultureManager.Core.Application.Features.UnitFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.States;
using AgricultureManager.Core.Application.Store.Features.UnitStore;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class UnitMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<UnitState> UnitState { get; set; } = default!;

        private RadzenDataGrid<UnitVm> _grid = default!;
        private UnitVm? _itemToEditOriginal;
        public string Title => "Einheit";

        private async Task DeleteRow(UnitVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveUnitCommand { Id = item.Id });
            if (response.Success)
                Dispatcher.Dispatch(new RemoveUnitAction(item.Id));
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(UnitVm item)
        {
            _itemToEditOriginal = Mapper.Map<UnitVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(UnitVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(UnitVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(UnitVm item)
        {
            var cmd = new UpdateUnitCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                Dispatcher.Dispatch(new UpdateUnitAction(response.Data));
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(UnitVm item)
        {
            var cmd = new AddUnitCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                Dispatcher.Dispatch(new AddUnitAction(response.Data));
            }
        }
    }
}
