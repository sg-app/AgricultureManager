using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Features.AccountMouvementsFeatures
{
    public record GetAccountMouvementListCommand(DateTime? StartDate, DateTime? EndDate, bool NoBookings) : IReq<IEnumerable<AccountMouvementVm>>
    {
    }

    public class GetAccountMouvementListCommandHandler(IAccountingDbContextFactory contextFactory, IMapper mapper) : IReqHandler<GetAccountMouvementListCommand, IEnumerable<AccountMouvementVm>>
    {
        public async Task<Response<IEnumerable<AccountMouvementVm>>> Handle(GetAccountMouvementListCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            var startDate = request.StartDate ?? DateTime.Now.AddDays(-30).Date;
            var endDate = request.EndDate ?? DateTime.Now.Date;

            var query = context.AccountMouvement
                .Include(i => i.Bookings)
                .Where(f => f.InputDate > startDate && f.InputDate < endDate);

            if (request.NoBookings)
                query = query.Where(f => f.Bookings!.Count == 0);

            var entities = await query.ToListAsync(cancellationToken);
            return Response.Success(mapper.Map<IEnumerable<AccountMouvementVm>>(entities));
        }
    }
}
