using Microsoft.Extensions.Hosting;

namespace AgricultureManager.Module.Accounting.Services
{
    public interface IAccountingHostedService : IHostedService
    {
        bool IsEnabled { get; set; }
    }
}