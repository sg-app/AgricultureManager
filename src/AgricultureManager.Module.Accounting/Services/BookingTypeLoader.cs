using AgricultureManager.Core.Application.Shared.Interfaces;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Services
{
    public class BookingTypeLoader(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IMasterDataLoader<BookingTypeVm>
    {
        public async Task<List<BookingTypeVm>> LoadDataAsync()
        {
            var dbContext = dbContextFactory.CreateDbContext();
            var entities = await dbContext.BookingType.ToListAsync();
            return mapper.Map<List<BookingTypeVm>>(entities);
        }
    }
}
