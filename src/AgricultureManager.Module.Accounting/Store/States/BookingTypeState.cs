using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Module.Accounting.Models;
using Fluxor;

namespace AgricultureManager.Module.Accounting.Store.States
{
    [FeatureState]
    public record BookingTypeState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<BookingTypeVm> BookingTypes { get; init; } = [];
        private BookingTypeState() { }
        public BookingTypeState(bool isLoading, bool isInitialized, IEnumerable<BookingTypeVm> bookingType)
        {
            IsLoading = isLoading;
            IsInitialized = isInitialized;
            BookingTypes = bookingType;
        }
    }
}
