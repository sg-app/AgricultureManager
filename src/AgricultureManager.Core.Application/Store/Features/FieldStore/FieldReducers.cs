using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.FieldStore
{
    public static class FieldReducers
    {
        [ReducerMethod(typeof(LoadFieldsDataAction))]
        public static FieldState LoadFieldsDataReducer(FieldState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static FieldState LoadFieldsDataResultReducer(FieldState state, LoadFieldsDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Fields = action.Fields };

        [ReducerMethod(typeof(LoadFieldDataResultFailAction))]
        public static FieldState LoadFieldDataResultFailReducer(FieldState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static FieldState AddFieldReducer(FieldState state, AddFieldAction action)
        {
            var list = state.Fields.ToList();
            var changed = list.Append(action.Field);
            return state with { Fields = changed };
        }

        [ReducerMethod]
        public static FieldState UpdateFieldReducer(FieldState state, UpdateFieldAction action)
        {
            var item = state.Fields.First(x => x.Id == action.Field.Id);
            var list = state.Fields.ToList();
            list.Remove(item);
            var changed = list.Append(action.Field);
            return state with { Fields = changed };
        }

        [ReducerMethod]
        public static FieldState RemoveFieldReducer(FieldState state, RemoveFieldAction action)
        {
            var item = state.Fields.First(x => x.Id == action.FieldId);
            var list = state.Fields.ToList();
            list.Remove(item);
            return state with { Fields = list };
        }
    }
}
