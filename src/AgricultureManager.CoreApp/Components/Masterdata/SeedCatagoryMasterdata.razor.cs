using AgricultureManager.Core.Application.Features.SeedCategoryFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class SeedCatagoryMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;

        private IList<SeedCategoryVm>? _data;
        private RadzenDataGrid<SeedCategoryVm> _grid = default!;
        private SeedCategoryVm? _itemToEditOriginal;
        public string Title => "Saatgutkategorie";
        protected async override Task OnInitializedAsync()
        {
            _data = (await Mediator.Send(new GetSeedCategoryListCommand())).Data;
        }

        private async Task DeleteRow(SeedCategoryVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveSeedCategoryCommand { Id = item.Id });
            if (response.Success && _data is not null)
                _data.Remove(item);
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(SeedCategoryVm item)
        {
            _itemToEditOriginal = Mapper.Map<SeedCategoryVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(SeedCategoryVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(SeedCategoryVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(SeedCategoryVm item)
        {
            var cmd = new UpdateSeedCategoryCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(SeedCategoryVm item)
        {
            var cmd = new AddSeedCategoryCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
                item.Id = response.Data.Id;
        }
    }
}
