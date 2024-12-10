using AgricultureManager.Core.Application.Shared.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.CultureStore
{
    public static class CultureReducers
    {
        [ReducerMethod(typeof(LoadCulturesDataAction))]
        public static CultureState LoadCulturesDataReducer(CultureState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static CultureState LoadCulturesDataResultReducer(CultureState state, LoadCulturesDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Cultures = action.Cultures };

        [ReducerMethod(typeof(LoadCultureDataResultFailAction))]
        public static CultureState LoadCultureDataResultFailReducer(CultureState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static CultureState AddCultureReducer(CultureState state, AddCultureAction action)
        {
            var list = state.Cultures.ToList();
            var changed = list.Append(action.Culture);
            return state with { Cultures = changed };
        }

        [ReducerMethod]
        public static CultureState UpdateCultureReducer(CultureState state, UpdateCultureAction action)
        {
            var item = state.Cultures.First(x => x.Id == action.Culture.Id);
            var list = state.Cultures.ToList();
            list.Remove(item);
            var changed = list.Append(action.Culture);
            return state with { Cultures = changed };
        }

        [ReducerMethod]
        public static CultureState RemoveCultureReducer(CultureState state, RemoveCultureAction action)
        {
            var item = state.Cultures.First(x => x.Id == action.CultureId);
            var list = state.Cultures.ToList();
            list.Remove(item);
            return state with { Cultures = list };
        }
    }
}
