using AgricultureManager.Module.Accounting.Models;

namespace AgricultureManager.Module.Accounting.Store.Features.BookingTypeStore
{
    public record LoadBookingTypesDataAction();
    public record LoadBookingTypesDataResultAction(IEnumerable<BookingTypeVm> BookingTypes);
    public record LoadBookingTypeDataResultFailAction();
    public record AddBookingTypeAction(BookingTypeVm BookingType);
    public record UpdateBookingTypeAction(BookingTypeVm BookingType);
    public record RemoveBookingTypeAction(Guid BookingTypeId);
}
