using AgricultureManager.Core.Application.Features.CultureFeatures;
using AgricultureManager.Core.Application.Features.FertilizerDetailFeatures;
using AgricultureManager.Core.Application.Features.FertilizerFeatures;
using AgricultureManager.Core.Application.Features.FertilizerToDetailFeatures;
using AgricultureManager.Core.Application.Features.FieldFeatures;
using AgricultureManager.Core.Application.Features.HarvestYearFeatures;
using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Features.PersonFeatures;
using AgricultureManager.Core.Application.Features.PlantProtectantFeatures;
using AgricultureManager.Core.Application.Features.SeedCategoryFeatures;
using AgricultureManager.Core.Application.Features.SeedFeatures;
using AgricultureManager.Core.Application.Features.SeedTechnologyFeatures;
using AgricultureManager.Core.Application.Features.UnitFeatures;
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
            CreateMap<AddCultureCommand, Culture>();
            CreateMap<UpdateCultureCommand, Culture>();
            CreateMap<CultureVm, CultureVm>();
            CreateMap<CultureVm, AddCultureCommand>();
            CreateMap<CultureVm, UpdateCultureCommand>();

            CreateMap<Fertilization, FertilizationVm>();

            CreateMap<Fertilizer, FertilizerVm>();
            CreateMap<AddFertilizerCommand, Fertilizer>();
            CreateMap<UpdateFertilizerCommand, Fertilizer>();
            CreateMap<FertilizerVm, FertilizerVm>();
            CreateMap<FertilizerVm, AddFertilizerCommand>();
            CreateMap<FertilizerVm, UpdateFertilizerCommand>();

            CreateMap<FertilizerDetail, FertilizerDetailVm>();
            CreateMap<AddFertilizerDetailCommand, FertilizerDetail>();
            CreateMap<UpdateFertilizerDetailCommand, FertilizerDetail>();
            CreateMap<FertilizerDetailVm, FertilizerDetailVm>();
            CreateMap<FertilizerDetailVm, AddFertilizerDetailCommand>();
            CreateMap<FertilizerDetailVm, UpdateFertilizerDetailCommand>();

            CreateMap<FertilizerToDetail, FertilizerToDetailVm>();
            CreateMap<AddFertilizerToDetailCommand, FertilizerToDetail>();
            CreateMap<UpdateFertilizerToDetailCommand, FertilizerToDetail>();
            CreateMap<FertilizerToDetailVm, FertilizerToDetailVm>();
            CreateMap<FertilizerToDetailVm, AddFertilizerToDetailCommand>();
            CreateMap<FertilizerToDetailVm, UpdateFertilizerToDetailCommand>();

            CreateMap<Field, FieldVm>();
            CreateMap<AddFieldCommand, Field>();
            CreateMap<UpdateFieldCommand, Field>();
            CreateMap<FieldVm, FieldVm>();
            CreateMap<FieldVm, AddFieldCommand>();
            CreateMap<FieldVm, UpdateFieldCommand>();

            CreateMap<Harvest, HarvestVm>();

            CreateMap<HarvestUnit, HarvestUnitVm>();

            CreateMap<HarvestYear, HarvestYearVm>();
            CreateMap<AddHarvestYearCommand, HarvestYear>();

            CreateMap<Parameter, ParameterVm>();
            CreateMap<AddParameterCommand, Parameter>();
            CreateMap<UpdateParameterCommand, Parameter>();

            CreateMap<Person, PersonVm>();
            CreateMap<AddPersonCommand, Person>();
            CreateMap<UpdatePersonCommand, Person>();
            CreateMap<PersonVm, PersonVm>();
            CreateMap<PersonVm, AddPersonCommand>();
            CreateMap<PersonVm, UpdatePersonCommand>();

            CreateMap<PlantProtectant, PlantProtectantVm>();
            CreateMap<AddPlantProtectantCommand, PlantProtectant>();
            CreateMap<UpdatePlantProtectantCommand, PlantProtectant>();
            CreateMap<PlantProtectantVm, PlantProtectantVm>();
            CreateMap<PlantProtectantVm, AddPlantProtectantCommand>();
            CreateMap<PlantProtectantVm, UpdatePlantProtectantCommand>();

            CreateMap<PlantProtection, PlantProtectionVm>();

            CreateMap<Seed, SeedVm>();
            CreateMap<Seed, SeedVm>();
            CreateMap<AddSeedCommand, Seed>();
            CreateMap<UpdateSeedCommand, Seed>();
            CreateMap<SeedVm, SeedVm>();
            CreateMap<SeedVm, AddSeedCommand>();
            CreateMap<SeedVm, UpdateSeedCommand>();

            CreateMap<SeedTechnology, SeedTechnologyVm>();
            CreateMap<AddSeedTechnologyCommand, SeedTechnology>();
            CreateMap<UpdateSeedTechnologyCommand, SeedTechnology>();
            CreateMap<SeedTechnologyVm, SeedTechnologyVm>();
            CreateMap<SeedTechnologyVm, AddSeedTechnologyCommand>();
            CreateMap<SeedTechnologyVm, UpdateSeedTechnologyCommand>();

            CreateMap<SeedCategory, SeedCategoryVm>();
            CreateMap<AddSeedCategoryCommand, SeedCategory>();
            CreateMap<UpdateSeedCategoryCommand, SeedCategory>();
            CreateMap<SeedCategoryVm, SeedCategoryVm>();
            CreateMap<SeedCategoryVm, AddSeedCategoryCommand>();
            CreateMap<SeedCategoryVm, UpdateSeedCategoryCommand>();

            CreateMap<Unit, UnitVm>();
            CreateMap<AddUnitCommand, Unit>();
            CreateMap<UpdateUnitCommand, Unit>();
            CreateMap<UnitVm, UnitVm>();
            CreateMap<UnitVm, AddUnitCommand>();
            CreateMap<UnitVm, UpdateUnitCommand>();

        }
    }
}
