using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Module.Accounting.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class BookingEditor
    {
        [Parameter] public BookingVm Booking { get; set; } = default!;
        [Parameter] public Decimal Amount { get; set; } = default!;
        [Parameter] public EventCallback<BookingVm> ButtonResult { get; set; }
        [Inject] public IMapper Mapper { get; set; } = default!;
        [Inject] protected IMasterdataService MasterdataService { get; set; } = default!;

        private BookingVm _booking = new();
        private decimal? _nettoValue;
        private bool _amountReadOnly;

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
                _booking.TaxRateId = MasterdataService.Get<TaxRateVm>().FirstOrDefault(f => f.IsDefault)?.Id ?? MasterdataService.Get<TaxRateVm>().First().Id;
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
                var taxRateValue = MasterdataService.Get<TaxRateVm>().Where(f => f.Id == _booking.TaxRateId).FirstOrDefault()?.TaxRateValue ?? 0;
                _booking.Amount = (decimal)_nettoValue * (1 + taxRateValue);
                _amountReadOnly = true;
            }
        }
    }
}
