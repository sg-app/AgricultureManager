﻿using AgricultureManager.Core.Application.Features.FertilizerFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.States;
using AgricultureManager.Core.Application.Store.Features.FertilizerStore;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class FertilizerMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<FertilizerState> FertilizerState { get; set; } = default!;


        private RadzenDataGrid<FertilizerVm> _grid = default!;
        private FertilizerVm? _itemToEditOriginal;
        public string Title => "Dünger";

        private async Task DeleteRow(FertilizerVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveFertilizerCommand { Id = item.Id });
            if (response.Success)
                Dispatcher.Dispatch(new RemoveFertilizerAction(item.Id));
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(FertilizerVm item)
        {
            _itemToEditOriginal = Mapper.Map<FertilizerVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(FertilizerVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(FertilizerVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(FertilizerVm item)
        {
            var cmd = new UpdateFertilizerCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                Dispatcher.Dispatch(new UpdateFertilizerAction(response.Data));
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(FertilizerVm item)
        {
            var cmd = new AddFertilizerCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                Dispatcher.Dispatch(new AddFertilizerAction(response.Data));
            }
        }

    }
}
