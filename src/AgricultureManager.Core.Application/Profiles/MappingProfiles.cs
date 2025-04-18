﻿using AgricultureManager.Core.Application.Features.CultureFeatures;
using AgricultureManager.Core.Application.Features.FertilizationFeatures;
using AgricultureManager.Core.Application.Features.FertilizerDetailFeatures;
using AgricultureManager.Core.Application.Features.FertilizerFeatures;
using AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures;
using AgricultureManager.Core.Application.Features.FertilizerPlaningSpecificationFeatures;
using AgricultureManager.Core.Application.Features.FertilizerToDetailFeatures;
using AgricultureManager.Core.Application.Features.FieldFeatures;
using AgricultureManager.Core.Application.Features.HarvestFeatures;
using AgricultureManager.Core.Application.Features.HarvestUnitFeatures;
using AgricultureManager.Core.Application.Features.HarvestYearFeatures;
using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Features.PersonFeatures;
using AgricultureManager.Core.Application.Features.PlantProtectantFeatures;
using AgricultureManager.Core.Application.Features.PlantProtectionFeatures;
using AgricultureManager.Core.Application.Features.SeedCategoryFeatures;
using AgricultureManager.Core.Application.Features.SeedFeatures;
using AgricultureManager.Core.Application.Features.SeedTechnologyFeatures;
using AgricultureManager.Core.Application.Features.UnitFeatures;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.Models.EditorModels;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<CompanyVm, CompanyVm>();

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

            CreateMap<FertilizerDetail, FertilizerDetailVm>().ReverseMap();
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
            CreateMap<AddHarvestUnitCommand, HarvestUnit>();
            CreateMap<UpdateHarvestUnitCommand, HarvestUnit>();
            CreateMap<HarvestUnitVm, HarvestUnitVm>();
            CreateMap<HarvestUnitVm, AddHarvestUnitCommand>();
            CreateMap<HarvestUnitVm, UpdateHarvestUnitCommand>();

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
            CreateMap<AddSeedCommand, Seed>();
            CreateMap<UpdateSeedCommand, Seed>();
            CreateMap<SeedVm, SeedVm>();
            CreateMap<SeedVm, AddSeedCommand>();
            CreateMap<SeedVm, UpdateSeedCommand>();

            CreateMap<Fertilization, FertilizationVm>();
            CreateMap<AddFertilizationCommand, Fertilization>();
            CreateMap<UpdateFertilizationCommand, Fertilization>();
            CreateMap<FertilizationVm, FertilizationVm>();
            CreateMap<FertilizationVm, AddFertilizationCommand>();
            CreateMap<FertilizationVm, UpdateFertilizationCommand>();

            CreateMap<PlantProtection, PlantProtectionVm>();
            CreateMap<AddPlantProtectionCommand, PlantProtection>();
            CreateMap<UpdatePlantProtectionCommand, PlantProtection>();
            CreateMap<PlantProtectionVm, PlantProtectionVm>();
            CreateMap<PlantProtectionVm, AddPlantProtectionCommand>();
            CreateMap<PlantProtectionVm, UpdatePlantProtectionCommand>();

            CreateMap<Harvest, HarvestVm>();
            CreateMap<AddHarvestCommand, Harvest>();
            CreateMap<UpdateHarvestCommand, Harvest>();
            CreateMap<HarvestVm, HarvestVm>();
            CreateMap<HarvestVm, AddHarvestCommand>();
            CreateMap<HarvestVm, UpdateHarvestCommand>();

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

            CreateMap<FertilizerPlaning, FertilizerPlaningVm>();
            CreateMap<AddFertilizerPlaningCommand, FertilizerPlaning>();
            CreateMap<UpdateFertilizerPlaningCommand, FertilizerPlaning>();
            CreateMap<FertilizerPlaningVm, FertilizerPlaningVm>();
            CreateMap<FertilizerPlaningVm, AddFertilizerPlaningCommand>();
            CreateMap<FertilizerPlaningVm, UpdateFertilizerPlaningCommand>();
            CreateMap<FertilizerPlaningSpecification, FertilizerPlaningSpecificationVm>().ReverseMap();
            CreateMap<AddFertilizerPlaningSpecificationCommand, FertilizerPlaningSpecification>();
            CreateMap<UpdateFertilizerPlaningSpecificationCommand, FertilizerPlaningSpecification>();
            CreateMap<FertilizerPlaningSpecificationVm, FertilizerPlaningSpecificationVm>();
            CreateMap<FertilizerPlaningSpecificationVm, AddFertilizerPlaningSpecificationCommand>();
            CreateMap<FertilizerPlaningSpecificationVm, UpdateFertilizerPlaningSpecificationCommand>();

            CreateMap<FertilizerPlaningVm, EditFertilizerPlaningVm>().ReverseMap();
            CreateMap<EditFertilizerPlaningVm, AddFertilizerPlaningCommand>().ReverseMap();
            CreateMap<EditFertilizerPlaningVm, FertilizerPlaning>();

        }
    }
}
