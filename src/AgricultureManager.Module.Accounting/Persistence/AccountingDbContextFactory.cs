using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Persistence
{
    public class AccountingDbContextFactory(IDbContextFactory<AccountingDbContext> dbContextFactory) : IAccountingDbContextFactory
    {
        public IAccountingDbContext CreateDbContext()
            => dbContextFactory.CreateDbContext();
    }
}
