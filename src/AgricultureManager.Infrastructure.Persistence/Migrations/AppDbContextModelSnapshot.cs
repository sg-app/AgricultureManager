﻿// <auto-generated />
using System;
using AgricultureManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgricultureManager.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Culture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Culture");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Fertilization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BBCH")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<double>("Dosage")
                        .HasColumnType("double");

                    b.Property<Guid>("FertilizerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HarvestUnitId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Setting")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<Guid?>("UnitId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("FertilizerId");

                    b.HasIndex("HarvestUnitId");

                    b.HasIndex("PersonId");

                    b.HasIndex("UnitId");

                    b.ToTable("Fertilization");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Fertilizer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Detail")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Fertilizer");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("SystemEntry")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("FertilizerDetail");

                    b.HasData(
                        new
                        {
                            Id = new Guid("04433ca8-714f-4007-bd93-672b2d10ff36"),
                            Comment = "Stickstoff",
                            Name = "N",
                            SystemEntry = true
                        },
                        new
                        {
                            Id = new Guid("0d69cc79-e5b4-4c84-afed-0f9397a611cb"),
                            Comment = "Phosphor",
                            Name = "P",
                            SystemEntry = true
                        },
                        new
                        {
                            Id = new Guid("1b5bb848-475d-4d77-bf53-d3d6ff09db46"),
                            Comment = "Kali",
                            Name = "K",
                            SystemEntry = true
                        },
                        new
                        {
                            Id = new Guid("8cfee622-ef2f-44ec-b6ca-92db0e8ee8fe"),
                            Comment = "Schwefel",
                            Name = "S",
                            SystemEntry = true
                        });
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerPlaning", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<float>("Dosage")
                        .HasColumnType("float");

                    b.Property<Guid>("FertilizerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HarvestUnitId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("UnitId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("FertilizerId");

                    b.HasIndex("HarvestUnitId");

                    b.HasIndex("UnitId");

                    b.ToTable("FertilizerPlaning");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerPlaningSpecification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FertilizerDetailId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HarvestUnitId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FertilizerDetailId");

                    b.HasIndex("HarvestUnitId");

                    b.ToTable("FertilizerPlaningSpecification");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerToDetail", b =>
                {
                    b.Property<Guid>("FertilizerDetailId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FertilizerId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("FertilizerDetailId", "FertilizerId");

                    b.HasIndex("FertilizerId");

                    b.ToTable("FertilizerToDetail");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("Id");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Harvest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("HarvestUnitId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<string>("Setting")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<Guid?>("UnitId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("HarvestUnitId");

                    b.HasIndex("PersonId");

                    b.HasIndex("UnitId");

                    b.ToTable("Harvest");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.HarvestUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float>("Area")
                        .HasColumnType("float");

                    b.Property<Guid>("CultureId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HarvestYearId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.HasIndex("FieldId");

                    b.HasIndex("HarvestYearId");

                    b.ToTable("HarvestUnit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.HarvestYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.HasKey("Id");

                    b.ToTable("HarvestYear");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Parameter", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Key");

                    b.ToTable("Parameter");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.PlantProtectant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ActiveSubstance")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PlantProtectantType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PlantProtectant");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.PlantProtection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BBCH")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<double>("Dosage")
                        .HasColumnType("double");

                    b.Property<Guid>("HarvestUnitId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PlantProtectantId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Setting")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<Guid?>("UnitId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("HarvestUnitId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PlantProtectantId");

                    b.HasIndex("UnitId");

                    b.ToTable("PlantProtection");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Seed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApprovalNumber")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("CultureId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("HarvestUnitId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsMainCulture")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<Guid?>("SeedCategoryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("SeedTechnologyId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Setting")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<Guid?>("UnitId")
                        .HasColumnType("char(36)");

                    b.Property<string>("VarietyName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.HasIndex("HarvestUnitId");

                    b.HasIndex("PersonId");

                    b.HasIndex("SeedCategoryId");

                    b.HasIndex("SeedTechnologyId");

                    b.HasIndex("UnitId");

                    b.ToTable("Seed");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.SeedCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("SeedCategory");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.SeedTechnology", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("SeedTechnology");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Fertilization", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.Fertilizer", "Fertilizer")
                        .WithMany()
                        .HasForeignKey("FertilizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestUnit", "HarvestUnit")
                        .WithMany("Fertilizations")
                        .HasForeignKey("HarvestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Fertilizer");

                    b.Navigation("HarvestUnit");

                    b.Navigation("Person");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerPlaning", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.Fertilizer", "Fertilizer")
                        .WithMany()
                        .HasForeignKey("FertilizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestUnit", "HarvestUnit")
                        .WithMany("FertilizerPlanings")
                        .HasForeignKey("HarvestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Fertilizer");

                    b.Navigation("HarvestUnit");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerPlaningSpecification", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.FertilizerDetail", "FertilizerDetail")
                        .WithMany()
                        .HasForeignKey("FertilizerDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestUnit", "HarvestUnit")
                        .WithMany("FertilizerPlaningSpecifications")
                        .HasForeignKey("HarvestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FertilizerDetail");

                    b.Navigation("HarvestUnit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerToDetail", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.FertilizerDetail", "FertilizerDetail")
                        .WithMany("FertilizerToDetails")
                        .HasForeignKey("FertilizerDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Fertilizer", "Fertilizer")
                        .WithMany("FertilizerToDetails")
                        .HasForeignKey("FertilizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fertilizer");

                    b.Navigation("FertilizerDetail");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Harvest", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestUnit", "HarvestUnit")
                        .WithMany("Harvests")
                        .HasForeignKey("HarvestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("HarvestUnit");

                    b.Navigation("Person");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.HarvestUnit", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.Culture", "Culture")
                        .WithMany()
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Field", "Field")
                        .WithMany("HarvestUnits")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestYear", "HarvestYear")
                        .WithMany("HarvestUnits")
                        .HasForeignKey("HarvestYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Culture");

                    b.Navigation("Field");

                    b.Navigation("HarvestYear");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.PlantProtection", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestUnit", "HarvestUnit")
                        .WithMany("PlantProtections")
                        .HasForeignKey("HarvestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.HasOne("AgricultureManager.Core.Domain.Entities.PlantProtectant", "PlantProtectant")
                        .WithMany()
                        .HasForeignKey("PlantProtectantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("HarvestUnit");

                    b.Navigation("Person");

                    b.Navigation("PlantProtectant");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Seed", b =>
                {
                    b.HasOne("AgricultureManager.Core.Domain.Entities.Culture", "Culture")
                        .WithMany()
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.HarvestUnit", "HarvestUnit")
                        .WithMany("Seeds")
                        .HasForeignKey("HarvestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.HasOne("AgricultureManager.Core.Domain.Entities.SeedCategory", "SeedCategory")
                        .WithMany()
                        .HasForeignKey("SeedCategoryId");

                    b.HasOne("AgricultureManager.Core.Domain.Entities.SeedTechnology", "SeedTechnology")
                        .WithMany()
                        .HasForeignKey("SeedTechnologyId");

                    b.HasOne("AgricultureManager.Core.Domain.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Culture");

                    b.Navigation("HarvestUnit");

                    b.Navigation("Person");

                    b.Navigation("SeedCategory");

                    b.Navigation("SeedTechnology");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Fertilizer", b =>
                {
                    b.Navigation("FertilizerToDetails");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.FertilizerDetail", b =>
                {
                    b.Navigation("FertilizerToDetails");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.Field", b =>
                {
                    b.Navigation("HarvestUnits");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.HarvestUnit", b =>
                {
                    b.Navigation("Fertilizations");

                    b.Navigation("FertilizerPlaningSpecifications");

                    b.Navigation("FertilizerPlanings");

                    b.Navigation("Harvests");

                    b.Navigation("PlantProtections");

                    b.Navigation("Seeds");
                });

            modelBuilder.Entity("AgricultureManager.Core.Domain.Entities.HarvestYear", b =>
                {
                    b.Navigation("HarvestUnits");
                });
#pragma warning restore 612, 618
        }
    }
}
