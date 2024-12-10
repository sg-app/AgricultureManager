using AgricultureManager.Core.Application.Store.States;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.Features.CompanyStore
{
    public static class CompanyReducers
    {
        [ReducerMethod(typeof(LoadCompanyDataAction))]
        public static CompanyState LoadCompanysDataReducer(CompanyState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static CompanyState LoadCompanysDataResultReducer(CompanyState state, LoadCompanyDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, Company = action.Company };

        [ReducerMethod(typeof(LoadCompanyDataResultFailAction))]
        public static CompanyState LoadCompanyDataResultFailReducer(CompanyState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static CompanyState UpdateCompanyReducer(CompanyState state, UpdateCompanyAction action) =>
            state with { Company = action.Company };
    }
}
