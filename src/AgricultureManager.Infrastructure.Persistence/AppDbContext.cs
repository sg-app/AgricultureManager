using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
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
        public DbSet<YearField> YearField { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlantProtectant>()
                .Property(p => p.PlantProtectantType)
                .HasConversion<string>();

            modelBuilder.Entity<YearField>()
                .HasKey(k => new { k.HarvestYearId, k.FieldId });

            modelBuilder.Entity<Fertilizer>()
                .HasMany(f => f.FertilizerDetails)
                .WithMany(f => f.Fertilizers)
                .UsingEntity<FertilizerToDetail>();
        }
    }
}
