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
            => state with { IsLoading = false, Peoples = action.Peoples };
    }
}
