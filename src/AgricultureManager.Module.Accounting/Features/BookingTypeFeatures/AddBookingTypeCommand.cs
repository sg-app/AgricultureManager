using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.BookingTypeFeatures
{
    public class AddBookingTypeCommand : IReq<BookingTypeVm>
    {
        public string Name { get; set; } = string.Empty;
        public string? Short { get; set; }

    }
    public class AddBookingTypeCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddBookingTypeCommand, BookingTypeVm>
    {
        public async Task<Response<BookingTypeVm>> Handle(AddBookingTypeCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<BookingType>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.BookingType.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<BookingTypeVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
