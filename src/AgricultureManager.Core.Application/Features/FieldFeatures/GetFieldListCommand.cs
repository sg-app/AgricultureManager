using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FieldFeatures
{
    public class GetFieldListCommand : IReq<IList<FieldVm>>
    {
    }

    public class GetFieldListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFieldListCommand, IList<FieldVm>>
    {
        public async Task<Response<IList<FieldVm>>> Handle(GetFieldListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Field.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<FieldVm>>(entities));
        }
    }
}
