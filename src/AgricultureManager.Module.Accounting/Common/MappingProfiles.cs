using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Features.TaxRateFeatures;
using AgricultureManager.Module.Accounting.Models;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TaxRate, TaxRateVm>();
            CreateMap<TaxRateVm, TaxRateVm>();
            CreateMap<TaxRateVm, AddTaxRateCommand>();
            CreateMap<TaxRateVm, UpdateTaxRateCommand>();
            CreateMap<AddTaxRateCommand, TaxRate>();
            CreateMap<UpdateTaxRateCommand, TaxRate>();

        }
    }
}
