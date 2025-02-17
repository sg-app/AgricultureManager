using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Domain.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext, IDataProtectionKeyContext
    {
        public DbSet<Culture> Culture { get; set; }
        public DbSet<Fertilization> Fertilization { get; set; }
        public DbSet<Fertilizer> Fertilizer { get; set; }
        public DbSet<FertilizerDetail> FertilizerDetail { get; set; }
        public DbSet<FertilizerToDetail> FertilizerToDetail { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<Harvest> Harvest { get; set; }
        public DbSet<HarvestUnit> HarvestUnit { get; set; }
        public DbSet<HarvestYear> HarvestYear { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PlantProtectant> PlantProtectant { get; set; }
        public DbSet<PlantProtection> PlantProtection { get; set; }
        public DbSet<Seed> Seed { get; set; }
        public DbSet<SeedCategory> SeedCategory { get; set; }
        public DbSet<SeedTechnology> SeedTechnology { get; set; }
        public DbSet<Unit> Unit { get; set; }


        public DbSet<FertilizerPlaning> FertilizerPlaning { get; set; }
        public DbSet<FertilizerPlaningSpecification> FertilizerPlaningSpecification { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlantProtectant>()
                .Property(p => p.PlantProtectantType)
                .HasConversion<string>();

            modelBuilder.Entity<Fertilizer>()
                .HasMany(f => f.FertilizerDetails)
                .WithMany(f => f.Fertilizers)
                .UsingEntity<FertilizerToDetail>();

            modelBuilder.Entity<FertilizerDetail>().HasData(
                new FertilizerDetail { Id = Guid.Parse("04433ca8-714f-4007-bd93-672b2d10ff36"), Name = "N", Comment = "Stickstoff", SystemEntry = true },
                new FertilizerDetail { Id = Guid.Parse("0d69cc79-e5b4-4c84-afed-0f9397a611cb"), Name = "P", Comment = "Phosphor", SystemEntry = true },
                new FertilizerDetail { Id = Guid.Parse("1b5bb848-475d-4d77-bf53-d3d6ff09db46"), Name = "K", Comment = "Kali", SystemEntry = true },
                new FertilizerDetail { Id = Guid.Parse("8cfee622-ef2f-44ec-b6ca-92db0e8ee8fe"), Name = "S", Comment = "Schwefel", SystemEntry = true }
            );
        }
    }
}
