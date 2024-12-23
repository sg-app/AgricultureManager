using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Features.BookingFeatures
{
    public record GetBookingListCommand(Guid AccountMouvementId) : IReq<ICollection<BookingVm>> { }

    public class GetBookingListCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetBookingListCommand, ICollection<BookingVm>>
    {
        public async Task<Response<ICollection<BookingVm>>> Handle(GetBookingListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Booking
                .AsNoTracking()
                .Where(f => f.AccountMouvementId == request.AccountMouvementId)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<ICollection<BookingVm>>(entities));
        }
    }
}
