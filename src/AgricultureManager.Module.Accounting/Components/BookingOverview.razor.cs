using AgricultureManager.Module.Accounting.Features.BookingFeatures;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.Features.BookingTypeStore;
using AgricultureManager.Module.Accounting.Store.Features.TaxRateStore;
using AgricultureManager.Module.Accounting.Store.States;
using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;


namespace AgricultureManager.Module.Accounting.Components
{
    public partial class BookingOverview
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<BookingTypeState> BookingTypeState { get; set; } = default!;
        [Inject] public IState<TaxRateState> TaxRateState { get; set; } = default!;
        [Parameter] public AccountMouvementVm AccountMouvement { get; set; } = default!;

        private ICollection<BookingVm> _bookings = [];
        private bool _onEdit;
        private BookingVm? _selectedItem;
        private bool _isLoading = false;
        private decimal _amount;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!BookingTypeState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadBookingTypesDataAction());
            if (!TaxRateState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadTaxRatesDataAction());

        }

        protected override async Task OnParametersSetAsync()
        {
            _isLoading = true;
            var response = await Mediator.Send(new GetBookingListCommand(AccountMouvement.Id));
            _bookings = response.Data ?? [];
            _amount = Math.Abs(AccountMouvement.Amount);
            _isLoading = false;
        }

        void OnInsertRowClick()
        {
            _selectedItem = new();
            _onEdit = true;
        }

        async Task ButtonResult(BookingVm item)
        {
            _onEdit = false;
            if (item != null)
            {
                item.AccountMouvementId = AccountMouvement.Id;
                if (item.Id == Guid.Empty)
                {
                    var command = Mapper.Map<AddBookingCommand>(item);
                    var response = await Mediator.Send(command);
                    if (response.Success && response.Data != null)
                    {
                        _bookings.Add(response.Data);
                    }
                }
                else
                {
                    var command = Mapper.Map<UpdateBookingCommand>(item);
                    var response = await Mediator.Send(command);
                    if (response.Success && response.Data != null)
                    {
                        Mapper.Map(response.Data, item);
                        //_bookings.Remove(_selectedItem);
                        //_bookings.Add(response.Data);
                    }
                }
            }
        }

        async Task Remove(BookingVm item)
        {
            var response = await Mediator.Send(new RemoveBookingCommand(item.Id));
            if (response.Success)
                _bookings.Remove(item);
        }
        void Edit(BookingVm item)
        {
            _selectedItem = item;
            _onEdit = true;
        }
    }
}
