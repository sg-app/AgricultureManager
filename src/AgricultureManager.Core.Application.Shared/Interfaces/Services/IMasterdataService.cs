using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureManager.Core.Application.Shared.Interfaces.Services
{
    public interface IMasterdataService
    {
        List<T>? Get<T>() where T: class;
        Task InitializeAsync();
        Task ReloadAsync<T>() where T : class;
    }
}
