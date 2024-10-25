using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.CultureFeatures
{
    public class GetCultureListCommand : IReq<IList<CultureVm>>
    {
    }

    public class GetCultureListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetCultureListCommand, IList<CultureVm>>
    {
        public async Task<Response<IList<CultureVm>>> Handle(GetCultureListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Culture.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<CultureVm>>(entities));
        }
    }
}
