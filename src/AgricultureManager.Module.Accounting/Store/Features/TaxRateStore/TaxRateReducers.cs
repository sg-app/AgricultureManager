using AgricultureManager.Module.Accounting.Store.States;
using Fluxor;

namespace AgricultureManager.Module.Accounting.Store.Features.TaxRateStore
{
    public static class TaxRateReducers
    {
        [ReducerMethod(typeof(LoadTaxRatesDataAction))]
        public static TaxRateState LoadTaxRatesDataReducer(TaxRateState state)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static TaxRateState LoadTaxRatesDataResultReducer(TaxRateState state, LoadTaxRatesDataResultAction action)
            => state with { IsInitialized = true, IsLoading = false, TaxRates = action.TaxRates };

        [ReducerMethod(typeof(LoadTaxRateDataResultFailAction))]
        public static TaxRateState LoadTaxRateDataResultFailReducer(TaxRateState state)
           => state with { IsLoading = false };

        [ReducerMethod]
        public static TaxRateState AddTaxRateReducer(TaxRateState state, AddTaxRateAction action)
        {
            var list = state.TaxRates.ToList();
            var changed = list.Append(action.TaxRate);
            return state with { TaxRates = changed };
        }

        [ReducerMethod]
        public static TaxRateState UpdateTaxRateReducer(TaxRateState state, UpdateTaxRateAction action)
        {
            var item = state.TaxRates.First(x => x.Id == action.TaxRate.Id);
            var list = state.TaxRates.ToList();
            list.Remove(item);
            var changed = list.Append(action.TaxRate);
            return state with { TaxRates = changed };
        }

        [ReducerMethod]
        public static TaxRateState RemoveTaxRateReducer(TaxRateState state, RemoveTaxRateAction action)
        {
            var item = state.TaxRates.First(x => x.Id == action.TaxRateId);
            var list = state.TaxRates.ToList();
            list.Remove(item);
            return state with { TaxRates = list };
        }
    }
}
