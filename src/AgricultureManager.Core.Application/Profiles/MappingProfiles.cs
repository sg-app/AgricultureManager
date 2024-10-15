using AgricultureManager.Core.Application.Features.CompanyFeatures;
using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Culture, CultureVm>();

            CreateMap<Fertilization, FertilizationVm>();

            CreateMap<Fertilizer, FertilizerVm>();

            CreateMap<Field, FieldVm>();

            CreateMap<Harvest, HarvestVm>();

            CreateMap<HarvestUnit, HarvestUnitVm>();

            CreateMap<HarvestYear, HarvestYearVm>();

            CreateMap<Parameter, ParameterVm>();
            CreateMap<AddParameterCommand, Parameter>();
            CreateMap<UpdateParameterCommand, Parameter>();

            CreateMap<Person, PersonVm>();

            CreateMap<PlantProtectant, PlantProtectantVm>();

            CreateMap<PlantProtection, PlantProtectionVm>();

            CreateMap<Seed, SeedVm>();

            CreateMap<SeedCategory, SeedCategoryVm>();

            CreateMap<SeedTechnology, SeedTechnologyVm>();

            CreateMap<Unit, UnitVm>();

            CreateMap<YearField, YearFieldVm>();



        }
    }
}
