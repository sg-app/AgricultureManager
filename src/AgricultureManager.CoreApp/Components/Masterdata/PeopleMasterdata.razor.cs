using AgricultureManager.Core.Application.Features.PersonFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class PeopleMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;

        private IList<PersonVm>? _data;
        private RadzenDataGrid<PersonVm> _grid = default!;
        private PersonVm? _itemToEditOriginal;
        public string Title => "Personen";
        protected async override Task OnInitializedAsync()
        {
            _data = (await Mediator.Send(new GetPersonListCommand())).Data;
        }

        private async Task DeleteRow(PersonVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemovePersonCommand { Id = item.Id });
            if (response.Success && _data is not null)
                _data.Remove(item);
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(PersonVm item)
        {
            _itemToEditOriginal = Mapper.Map<PersonVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(PersonVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(PersonVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(PersonVm item)
        {
            var cmd = new UpdatePersonCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(PersonVm item)
        {
            var cmd = new AddPersonCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
                item.Id = response.Data.Id;
        }
    }
}
