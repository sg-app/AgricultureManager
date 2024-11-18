﻿using AgricultureManager.Core.Application.Features.PlantProtectantFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class PlantProtectantMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;

        private IList<PlantProtectantVm>? _data;
        private RadzenDataGrid<PlantProtectantVm> _grid = default!;
        private PlantProtectantVm? _itemToEditOriginal;
        public string Title => "Pflanzenschutzmittel";
        protected async override Task OnInitializedAsync()
        {
            _data = (await Mediator.Send(new GetPlantProtectantListCommand())).Data;
        }

        private async Task DeleteRow(PlantProtectantVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemovePlantProtectantCommand { Id = item.Id });
            if (response.Success && _data is not null)
                _data.Remove(item);
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(PlantProtectantVm item)
        {
            _itemToEditOriginal = Mapper.Map<PlantProtectantVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(PlantProtectantVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(PlantProtectantVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(PlantProtectantVm item)
        {
            var cmd = new UpdatePlantProtectantCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(PlantProtectantVm item)
        {
            var cmd = new AddPlantProtectantCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
                item.Id = response.Data.Id;
        }
    }
}