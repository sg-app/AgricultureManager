using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
