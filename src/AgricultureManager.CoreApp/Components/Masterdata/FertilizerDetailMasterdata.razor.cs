using AgricultureManager.Core.Application.Features.FertilizerDetailFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.States;
using AgricultureManager.Core.Application.Store.Features.FertilizerDetailStore;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class FertilizerDetailMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<FertilizerDetailState> FertilizerDetailState { get; set; } = default!;

        private RadzenDataGrid<FertilizerDetailVm> _grid = default!;
        private FertilizerDetailVm? _itemToEditOriginal;
        public string Title => "Dünger Details";

        private async Task DeleteRow(FertilizerDetailVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveFertilizerDetailCommand { Id = item.Id });
            if (response.Success)
                Dispatcher.Dispatch(new RemoveFertilizerDetailAction(item.Id));
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(FertilizerDetailVm item)
        {
            _itemToEditOriginal = Mapper.Map<FertilizerDetailVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(FertilizerDetailVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(FertilizerDetailVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(FertilizerDetailVm item)
        {
            var cmd = new UpdateFertilizerDetailCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                Dispatcher.Dispatch(new UpdateFertilizerDetailAction(response.Data));
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(FertilizerDetailVm item)
        {
            var cmd = new AddFertilizerDetailCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                Dispatcher.Dispatch(new AddFertilizerDetailAction(response.Data));
            }
        }
    }
}
