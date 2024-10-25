using AgricultureManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AgricultureManager.Core.Application.Shared.Interfaces.Persistence
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        ValueTask<object?> FindAsync(Type entityType, object?[]? keyValues, CancellationToken cancellationToken);
        EntityEntry Remove(object entity);
        EntityEntry Add(object entity);

        DbSet<Culture> Culture { get; set; }
        DbSet<Fertilization> Fertilization { get; set; }
        DbSet<Fertilizer> Fertilizer { get; set; }
        DbSet<FertilizerDetail> FertilizerDetail { get; set; }
        DbSet<FertilizerToDetail> FertilizerToDetail { get; set; }
        DbSet<Field> Field { get; set; }
        DbSet<Harvest> Harvest { get; set; }
        DbSet<HarvestUnit> HarvestUnit { get; set; }
        DbSet<HarvestYear> HarvestYear { get; set; }
        DbSet<Parameter> Parameter { get; set; }
        DbSet<Person> Person { get; set; }
        DbSet<PlantProtectant> PlantProtectant { get; set; }
        DbSet<PlantProtection> PlantProtection { get; set; }
        DbSet<Seed> Seed { get; set; }
        DbSet<SeedCategory> SeedCategory { get; set; }
        DbSet<SeedTechnology> SeedTechnology { get; set; }
        DbSet<Unit> Unit { get; set; }
        DbSet<YearField> YearField { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
