using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.BookingTypeFeatures
{
    public class UpdateBookingTypeCommand : IReq<BookingTypeVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Short { get; set; }
    }

    public class UpdateBookingTypeCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateBookingTypeCommand, BookingTypeVm>
    {
        public async Task<Response<BookingTypeVm>> Handle(UpdateBookingTypeCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.BookingType.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<BookingTypeVm>("Buchungstyp nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.BookingType.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<BookingTypeVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
