using AgricultureManager.Core.Application.Shared.Interfaces;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Services
{
    public class BookingTypeLoader(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IMasterDataLoader
    {
        public Type ViewModelType => typeof(BookingTypeVm);

        public async Task<object> LoadDataAsync()
        {
            var dbContext = dbContextFactory.CreateDbContext();
            var entities = await dbContext.BookingType.ToListAsync();
            return mapper.Map<List<BookingTypeVm>>(entities);
        }
    }
}
