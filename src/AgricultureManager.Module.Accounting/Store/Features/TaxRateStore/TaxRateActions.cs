using AgricultureManager.Module.Accounting.Models;

namespace AgricultureManager.Module.Accounting.Store.Features.TaxRateStore
{
    public record LoadTaxRatesDataAction();
    public record LoadTaxRatesDataResultAction(IEnumerable<TaxRateVm> TaxRates);
    public record LoadTaxRateDataResultFailAction();
    public record AddTaxRateAction(TaxRateVm TaxRate);
    public record UpdateTaxRateAction(TaxRateVm TaxRate);
    public record RemoveTaxRateAction(Guid TaxRateId);
}
