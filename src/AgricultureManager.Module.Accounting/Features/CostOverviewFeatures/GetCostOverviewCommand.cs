using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Features.CostOverviewFeatures
{
    public record GetCostOverviewCommand(HarvestYearVm HarvestYear) : IReq<IEnumerable<CostOverviewVm>> { }

    public class GetCostOverviewCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetCostOverviewCommand, IEnumerable<CostOverviewVm>>
    {
        public async Task<Response<IEnumerable<CostOverviewVm>>> Handle(GetCostOverviewCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var (start, end) = GetFiscalTimeFrame(request.HarvestYear);
            var entities = await dbContext.AccountMouvement
                .AsNoTracking()
                .Where(f=>f.InputDate >= start && f.InputDate <= end)
                .Include(f=>f.Bookings)
                .ThenInclude(f => f.BookingType)
                .ToListAsync(cancellationToken);

            var groupedData = entities
               .SelectMany(am => am.Bookings)
               .GroupBy(b => b.BookingType.CostType)
               .Select(g => new CostOverviewVm
               {
                   CostType = (CostTypeVm?)g.Key,
                   TotalAmount = g.Key == CostType.Income ? g.Sum(b => b.Amount) : g.Sum(b => b.Amount) * -1,
                   Bookings = mapper.Map<ICollection<BookingVm>>(g.ToList())
               })
               .OrderBy(o=>o.CostType)
               .AsEnumerable();

            return Response.Success(groupedData);
        }
        private static (DateTime Start, DateTime End) GetFiscalTimeFrame(HarvestYearVm harvestYear)
        {
            var start = new DateTime(int.Parse(harvestYear.Year) - 1, 7, 1);
            var end = new DateTime(int.Parse(harvestYear.Year), 6, 30);
            return (start, end);
        }
    }
}
