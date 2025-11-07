using AgricultureManager.Core.Application.Features.SeedTechnologyFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class SeedTechnologyMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] protected IMasterdataService MasterdataService { get; set; } = default!;

        private RadzenDataGrid<SeedTechnologyVm> _grid = default!;
        private SeedTechnologyVm? _itemToEditOriginal;
        public string Title => "Saattechnologie";

        private async Task DeleteRow(SeedTechnologyVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveSeedTechnologyCommand { Id = item.Id });
            if (response.Success)
                await MasterdataService.ReloadAsync<SeedTechnologyVm>();
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(SeedTechnologyVm item)
        {
            _itemToEditOriginal = Mapper.Map<SeedTechnologyVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(SeedTechnologyVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(SeedTechnologyVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(SeedTechnologyVm item)
        {
            var cmd = new UpdateSeedTechnologyCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                await MasterdataService.ReloadAsync<SeedTechnologyVm>();
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(SeedTechnologyVm item)
        {
            var cmd = new AddSeedTechnologyCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                await MasterdataService.ReloadAsync<SeedTechnologyVm>();
            }
        }
    }
}
