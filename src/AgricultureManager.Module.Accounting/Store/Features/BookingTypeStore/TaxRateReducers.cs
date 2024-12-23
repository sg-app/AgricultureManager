using AgricultureManager.Module.Accounting.Store.States;
using Fluxor;

namespace AgricultureManager.Module.Accounting.Store.Features.BookingTypeStore
{
    public static class BookingTypeReducers
    {
        [ReducerMethod(typeof(LoadBookingTypesDataAction))]
        public static BookingTypeState LoadBookingTypesDataReducer(BookingTypeState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static BookingTypeState LoadBookingTypesDataResultReducer(BookingTypeState state, LoadBookingTypesDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, BookingTypes = action.BookingTypes };

        [ReducerMethod(typeof(LoadBookingTypeDataResultFailAction))]
        public static BookingTypeState LoadBookingTypeDataResultFailReducer(BookingTypeState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static BookingTypeState AddBookingTypeReducer(BookingTypeState state, AddBookingTypeAction action)
        {
            var list = state.BookingTypes.ToList();
            var changed = list.Append(action.BookingType);
            return state with { BookingTypes = changed };
        }

        [ReducerMethod]
        public static BookingTypeState UpdateBookingTypeReducer(BookingTypeState state, UpdateBookingTypeAction action)
        {
            var item = state.BookingTypes.First(x => x.Id == action.BookingType.Id);
            var list = state.BookingTypes.ToList();
            list.Remove(item);
            var changed = list.Append(action.BookingType);
            return state with { BookingTypes = changed };
        }

        [ReducerMethod]
        public static BookingTypeState RemoveBookingTypeReducer(BookingTypeState state, RemoveBookingTypeAction action)
        {
            var item = state.BookingTypes.First(x => x.Id == action.BookingTypeId);
            var list = state.BookingTypes.ToList();
            list.Remove(item);
            return state with { BookingTypes = list };
        }
    }
}
