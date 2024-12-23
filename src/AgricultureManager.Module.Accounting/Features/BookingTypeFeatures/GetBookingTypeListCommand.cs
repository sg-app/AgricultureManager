using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Features.BookingTypeFeatures
{
    public record GetBookingTypeListCommand : IReq<IEnumerable<BookingTypeVm>> { }

    public class GetBookingTypeListCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetBookingTypeListCommand, IEnumerable<BookingTypeVm>>
    {
        public async Task<Response<IEnumerable<BookingTypeVm>>> Handle(GetBookingTypeListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.BookingType.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IEnumerable<BookingTypeVm>>(entities));
        }
    }
}
