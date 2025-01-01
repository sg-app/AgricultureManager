using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.BookingFeatures
{
    public class AddBookingCommand : IReq<BookingVm>
    {
        public Guid AccountMouvementId { get; set; }
        public Guid BookingTypeId { get; set; }
        public decimal Amount { get; set; }
        public Guid TaxRateId { get; set; }

    }
    public class AddBookingCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddBookingCommand, BookingVm>
    {
        public async Task<Response<BookingVm>> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<Booking>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.Booking.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<BookingVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
