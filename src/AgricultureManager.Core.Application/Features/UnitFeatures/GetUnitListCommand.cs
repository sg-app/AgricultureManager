using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.UnitFeatures
{
    public class GetUnitListCommand : IReq<IList<UnitVm>>
    {
    }

    public class GetUnitListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetUnitListCommand, IList<UnitVm>>
    {
        public async Task<Response<IList<UnitVm>>> Handle(GetUnitListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Unit.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<UnitVm>>(entities));
        }
    }
}
