using AgricultureManager.Module.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AgricultureManager.Module.Accounting.Persistence
{
    public class AccountingDbContext : DbContext, IAccountingDbContext
    {
        public DbSet<AccountMouvement> AccountMouvement { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingType> BookingType { get; set; }
        public DbSet<TaxRate> TaxRate { get; set; }
        public DbSet<StatementOfAccountDocument> StatementOfAccountDocument { get; set; }
        public DbSet<Account> Account { get; set; }
        DatabaseFacade IAccountingDbContext.Database { get => this.Database; }

        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountMouvement>(f =>
            {
                f.HasKey(f => f.Id);
                f.HasAlternateKey(f => new { f.InputDate, f.Amount, f.Description });
                f.Property(x => x.Id).HasDefaultValueSql("UUID()");
            });

            modelBuilder.Entity<StatementOfAccountDocument>(f =>
            {
                f.HasKey(f => f.Id);
                f.HasAlternateKey(f => new { f.Month, f.Year });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
