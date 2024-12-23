using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;

namespace AgricultureManager.Module.Accounting.Features.BookingFeatures
{
    public record RemoveBookingCommand(Guid Id) : IReq { }

    public class RemoveBookingCommandHandler(IAccountingDbContextFactory dbContextFactory) : IReqHandler<RemoveBookingCommand>
    {
        public async Task<ResponseLess> Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Booking.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Buchung nicht gefunden.");

            dbContext.Booking.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
