using AgricultureManager.Module.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AgricultureManager.Module.Accounting.Persistence
{
    public interface IAccountingDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        ValueTask<object?> FindAsync(Type entityType, object?[]? keyValues, CancellationToken cancellationToken);
        EntityEntry Remove(object entity);
        EntityEntry Add(object entity);

        DatabaseFacade Database { get; }

        DbSet<AccountMouvement> AccountMouvement { get; set; }
        DbSet<Document> Document { get; set; }
        DbSet<Booking> Booking { get; set; }
        DbSet<BookingType> BookingType { get; set; }
        DbSet<TaxRate> TaxRate { get; set; }
        DbSet<StatementOfAccountDocument> StatementOfAccountDocument { get; set; }
        DbSet<Account> Account { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
