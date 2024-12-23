using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;
using Microsoft.Extensions.Logging;

namespace AgricultureManager.Module.Accounting.Features.DocumentFeatures
{
    public record RemoveDocumentCommand(Guid Id) : IReq { }

    public class RemoveDocumentCommandHandler(IAccountingDbContextFactory dbContextFactory, ILogger<RemoveDocumentCommandHandler> logger) : IReqHandler<RemoveDocumentCommand>
    {
        public async Task<ResponseLess> Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Document.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Dokument nicht gefunden.");

            dbContext.Document.Remove(entity);
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
