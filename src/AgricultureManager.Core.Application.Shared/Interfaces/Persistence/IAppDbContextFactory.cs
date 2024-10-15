using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureManager.Core.Application.Shared.Interfaces.Persistence
{
    public interface IAppDbContextFactory
    {
        IAppDbContext CreateDbContext();
    }
}
