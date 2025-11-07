using AgricultureManager.Core.Application.Shared.Interfaces;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Services
{
    public class TaxRateLoader(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IMasterDataLoader
    {
        public Type ViewModelType => typeof(TaxRateVm);

        public async Task<object> LoadDataAsync()
        {
            var dbContext = dbContextFactory.CreateDbContext();
            var entities = await dbContext.TaxRate.ToListAsync();
            return mapper.Map<List<TaxRateVm>>(entities);
        }
    }
}
