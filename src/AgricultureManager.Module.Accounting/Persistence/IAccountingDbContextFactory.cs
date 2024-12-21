namespace AgricultureManager.Module.Accounting.Persistence
{
    public interface IAccountingDbContextFactory
    {
        IAccountingDbContext CreateDbContext();
    }
}
