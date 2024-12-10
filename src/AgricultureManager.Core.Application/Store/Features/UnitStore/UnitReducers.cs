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
            => state with { IsInitialized = true, IsLoading = false, Units = action.Units };

        [ReducerMethod(typeof(LoadUnitDataResultFailAction))]
        public static UnitState LoadUnitDataResultFailReducer(UnitState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static UnitState AddUnitReducer(UnitState state, AddUnitAction action)
        {
            var list = state.Units.ToList();
            var changed = list.Append(action.Unit);
            return state with { Units = changed };
        }

        [ReducerMethod]
        public static UnitState UpdateUnitReducer(UnitState state, UpdateUnitAction action)
        {
            var item = state.Units.First(x => x.Id == action.Unit.Id);
            var list = state.Units.ToList();
            list.Remove(item);
            var changed = list.Append(action.Unit);
            return state with { Units = changed };
        }

        [ReducerMethod]
        public static UnitState RemoveUnitReducer(UnitState state, RemoveUnitAction action)
        {
            var item = state.Units.First(x => x.Id == action.UnitId);
            var list = state.Units.ToList();
            list.Remove(item);
            return state with { Units = list };
        }
    }
}
