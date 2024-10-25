using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Infrastructure.Persistence
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public AppDbContextFactory(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IAppDbContext CreateDbContext()
        {
            return _dbContextFactory.CreateDbContext();
        }
    }
}
