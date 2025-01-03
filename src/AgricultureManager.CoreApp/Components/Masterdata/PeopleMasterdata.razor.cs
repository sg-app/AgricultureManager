﻿using AgricultureManager.Core.Application.Features.PersonFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.States;
using AgricultureManager.Core.Application.Store.Features.PeopleStore;
using AgricultureManager.Module.Api.Interfaces;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace AgricultureManager.CoreApp.Components.Masterdata
{
    public partial class PeopleMasterdata : IMasterdata
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public DialogService DialogService { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<PeopleState> PeopleState { get; set; } = default!;

        private RadzenDataGrid<PersonVm> _grid = default!;
        private PersonVm? _itemToEditOriginal;
        public string Title => "Personen";

        private async Task DeleteRow(PersonVm item)
        {
            var dialogResponse = await DialogService.Confirm("Soll der Datensatz wirklich gelöscht werden?", "Datensatz löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (dialogResponse is null || dialogResponse == false)
                return;

            var response = await Mediator.Send(new RemovePersonCommand { Id = item.Id });
            if (response.Success)
                Dispatcher.Dispatch(new RemovePeopleAction(item.Id));
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
            else if (response.Success && response.Data is not null)
                Dispatcher.Dispatch(new UpdatePeopleAction(response.Data));
            _itemToEditOriginal = null;
        }
        private async Task OnCreateRow(PersonVm item)
        {
            var cmd = new AddPersonCommand();
            Mapper.Map(item, cmd);
            var response = await Mediator.Send(cmd);
            if (response.Success && response.Data is not null)
            {
                item.Id = response.Data.Id;
                Dispatcher.Dispatch(new AddPeopleAction(response.Data));
            }
        }
    }
}
