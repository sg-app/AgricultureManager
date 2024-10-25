namespace AgricultureManager.Core.Application.Shared.Interfaces.Persistence
{
    public interface IAppDbContextFactory
    {
        IAppDbContext CreateDbContext();
    }
}
