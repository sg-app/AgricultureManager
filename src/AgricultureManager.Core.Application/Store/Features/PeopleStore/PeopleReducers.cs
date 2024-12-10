using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.PeopleStore
{
    public static class PeopleReducers
    {
        [ReducerMethod(typeof(LoadPeoplesDataAction))]
        public static PeopleState LoadPeoplesDataReducer(PeopleState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static PeopleState LoadPeoplesDataResultReducer(PeopleState state, LoadPeoplesDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Peoples = action.Peoples };

        [ReducerMethod(typeof(LoadPeopleDataResultFailAction))]
        public static PeopleState LoadPeopleDataResultFailReducer(PeopleState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static PeopleState AddPeopleReducer(PeopleState state, AddPeopleAction action)
        {
            var list = state.Peoples.ToList();
            var changed = list.Append(action.People);
            return state with { Peoples = changed };
        }

        [ReducerMethod]
        public static PeopleState UpdatePeopleReducer(PeopleState state, UpdatePeopleAction action)
        {
            var item = state.Peoples.First(x => x.Id == action.People.Id);
            var list = state.Peoples.ToList();
            list.Remove(item);
            var changed = list.Append(action.People);
            return state with { Peoples = changed };
        }

        [ReducerMethod]
        public static PeopleState RemovePeopleReducer(PeopleState state, RemovePeopleAction action)
        {
            var item = state.Peoples.First(x => x.Id == action.PersonId);
            var list = state.Peoples.ToList();
            list.Remove(item);
            return state with { Peoples = list };
        }
    }
}
