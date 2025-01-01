using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.Features.BookingTypeStore;
using AgricultureManager.Module.Accounting.Store.Features.TaxRateStore;
using AgricultureManager.Module.Accounting.Store.States;
using AutoMapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class BookingEditor
    {
        [Parameter] public BookingVm Booking { get; set; } = default!;
        [Parameter] public Decimal Amount { get; set; } = default!;
        [Parameter] public EventCallback<BookingVm> ButtonResult { get; set; }
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] public IState<BookingTypeState> BookingTypeState { get; set; } = default!;
        [Inject] public IState<TaxRateState> TaxRateState { get; set; } = default!;

        private BookingVm _booking = new();
        private decimal? _nettoValue;
        private bool _amountReadOnly;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!BookingTypeState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadBookingTypesDataAction());
            if (!TaxRateState.Value.IsInitialized)
                Dispatcher.Dispatch(new LoadTaxRatesDataAction());

        }
        protected override void OnParametersSet()
        {

            Booking ??= new();
            Mapper.Map(Booking, _booking);

            if (Booking.Id == Guid.Empty)
            {
                _booking.Amount = Amount;
            }
            // Preselect Tax
            if (Booking.TaxRateId == Guid.Empty)
                _booking.TaxRateId = TaxRateState.Value.TaxRates.FirstOrDefault(f => f.IsDefault)?.Id ?? TaxRateState.Value.TaxRates.First().Id;
        }

        void OnSaveCancelClick(bool save)
        {
            if (save)
                ButtonResult.InvokeAsync(_booking);
            else
                ButtonResult.InvokeAsync(null);
        }

        private void StartCalculation()
        {
            _amountReadOnly = false;
            if (_nettoValue != null && _nettoValue > 0 && _booking.TaxRateId != Guid.Empty)
            {
                var taxRateValue = TaxRateState.Value.TaxRates.Where(f => f.Id == _booking.TaxRateId).FirstOrDefault()?.TaxRateValue ?? 0;
                _booking.Amount = (decimal)_nettoValue * (1 + taxRateValue);
                _amountReadOnly = true;
            }
        }
    }
}
