using AgricultureManager.Core.Application.Features.FieldFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class FieldMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;

        private IList<FieldVm>? _data;
        private RadzenDataGrid<FieldVm> _grid = default!;
        private FieldVm? _itemToEditOriginal;
        public string Title => "Schlag";
        protected async override Task OnInitializedAsync()
        {
            _data = (await Mediator.Send(new GetFieldListCommand())).Data;
        }

        private async Task DeleteRow(FieldVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveFieldCommand { Id = item.Id });
            if (response.Success && _data is not null)
                _data.Remove(item);
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(FieldVm item)
        {
            _itemToEditOriginal = Mapper.Map<FieldVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(FieldVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(FieldVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(FieldVm item)
        {
            var cmd = new UpdateFieldCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(FieldVm item)
        {
            var cmd = new AddFieldCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
                item.Id = response.Data.Id;
        }
    }
}
