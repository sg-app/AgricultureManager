using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.BookingFeatures
{
    public class UpdateBookingCommand : IReq<BookingVm>
    {
        public Guid Id { get; set; }
        public Guid AccountMouvementId { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public Guid TaxRateId { get; set; }
    }

    public class UpdateBookingCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateBookingCommand, BookingVm>
    {
        public async Task<Response<BookingVm>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.Booking.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<BookingVm>("Buchung nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.Booking.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<BookingVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
