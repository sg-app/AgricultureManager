using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;

namespace AgricultureManager.Module.Accounting.Features.BookingTypeFeatures
{
    public record RemoveBookingTypeCommand(Guid Id) : IReq { }

    public class RemoveBookingTypeCommandHandler(IAccountingDbContextFactory dbContextFactory) : IReqHandler<RemoveBookingTypeCommand>
    {
        public async Task<ResponseLess> Handle(RemoveBookingTypeCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.BookingType.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Buchungstyp nicht gefunden.");

            dbContext.BookingType.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
