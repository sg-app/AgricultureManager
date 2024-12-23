using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace AgricultureManager.Module.Accounting.Features.DocumentFeatures
{
    public record GetDocumentListByMouvementIdCommand(Guid AccountMouvementId) : IReq<ICollection<DocumentVm>>
    {
    }

    public class GetDocumentListByMouvementIdCommandHandler(IAccountingDbContextFactory contextFactory, IMapper mapper) : IReqHandler<GetDocumentListByMouvementIdCommand, ICollection<DocumentVm>>
    {
        public async Task<Response<ICollection<DocumentVm>>> Handle(GetDocumentListByMouvementIdCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            var entities = await context.Document
                .Where(f => f.AccountMouvementId == request.AccountMouvementId)
                .ToListAsync(cancellationToken);
            return Response.Success(mapper.Map<ICollection<DocumentVm>>(entities));

            throw new NotImplementedException();
        }
    }
}
