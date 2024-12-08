using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.UnitStore
{
    public static class UnitReducers
    {
        [ReducerMethod(typeof(LoadUnitsDataAction))]
        public static UnitState LoadUnitsDataReducer(UnitState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static UnitState LoadUnitsDataResultReducer(UnitState state, LoadUnitsDataResultAction action)
            => state with { IsLoading = false, Units = action.Units };
    }
}
