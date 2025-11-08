using AgricultureManager.Core.Application.Shared.Interfaces;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Module.Accounting.Features.BookingTypeFeatures;
using AgricultureManager.Module.Accounting.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.Module.Accounting.Components.Masterdata
{
    public partial class BookingTypeMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] protected IMasterdataService MasterdataService { get; set; } = default!;

        private RadzenDataGrid<BookingTypeVm> _grid = default!;
        private BookingTypeVm? _itemToEditOriginal;
        public string Title => "Buchungstypen";

     
        private async Task DeleteRow(BookingTypeVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemoveBookingTypeCommand(item.Id));
            if (response.Success)
                await MasterdataService.ReloadAsync<BookingTypeVm>();
            else
                _grid.CancelEditRow(item);
            await _grid.Reload();
        }

        private async Task InsertRow() =>
            await _grid.InsertRow(new());

        private async Task EditRow(BookingTypeVm item)
        {
            _itemToEditOriginal = Mapper.Map<BookingTypeVm>(item);
            await _grid.EditRow(item);
        }

        private void CancelEditRow(BookingTypeVm item)
        {
            _grid.CancelEditRow(item);
            Mapper.Map(_itemToEditOriginal, item);
            _itemToEditOriginal = null;
        }

        private async Task SaveRow(BookingTypeVm item) =>
            await _grid.UpdateRow(item);

        private async Task OnUpdateRow(BookingTypeVm item)
        {
            var cmd = new UpdateBookingTypeCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (!response.Success)
                await _grid.Reload();
            else if (response.Success && response.Data is not null)
                await MasterdataService.ReloadAsync<BookingTypeVm>();
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(BookingTypeVm item)
        {
            var cmd = new AddBookingTypeCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                await MasterdataService.ReloadAsync<BookingTypeVm>();
            }
        }
    }
}
