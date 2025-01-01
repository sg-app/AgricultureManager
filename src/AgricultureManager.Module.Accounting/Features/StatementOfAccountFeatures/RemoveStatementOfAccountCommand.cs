using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;
using Microsoft.Extensions.Logging;

namespace AgricultureManager.Module.Accounting.Features.StatementOfAccountFeatures
{
    public record RemoveStatementOfAccountCommand(Guid Id) : IReq { }

    public class RemoveStatementOfAccountCommandHandler(IAccountingDbContextFactory dbContextFactory, ILogger<RemoveStatementOfAccountCommandHandler> logger) : IReqHandler<RemoveStatementOfAccountCommand>
    {
        public async Task<ResponseLess> Handle(RemoveStatementOfAccountCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.StatementOfAccountDocument.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Dokument nicht gefunden.");

            dbContext.StatementOfAccountDocument.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            try
            {
                if (File.Exists(entity.Documentpath))
                {
                    File.Delete(entity.Documentpath);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "File {file} could not deleted.", entity.Documentpath);
            }

            return Response.Success();
        }
    }
}
