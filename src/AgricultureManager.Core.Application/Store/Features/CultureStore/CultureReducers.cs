using AgricultureManager.Core.Application.Store.States;
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
            => state with { IsLoading = false, Cultures = action.Cultures };
    }
}
