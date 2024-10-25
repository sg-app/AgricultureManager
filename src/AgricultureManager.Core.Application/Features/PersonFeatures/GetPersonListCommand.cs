using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.PersonFeatures
{
    public class GetPersonListCommand : IReq<IList<PersonVm>>
    {
    }

    public class GetPersonListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetPersonListCommand, IList<PersonVm>>
    {
        public async Task<Response<IList<PersonVm>>> Handle(GetPersonListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Person.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<PersonVm>>(entities));
        }
    }
}
