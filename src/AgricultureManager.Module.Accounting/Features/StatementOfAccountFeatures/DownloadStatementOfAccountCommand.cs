using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;

namespace AgricultureManager.Module.Accounting.Features.StatementOfAccountFeatures
{
    public record DownloadStatementOfAccountCommand(Guid StatementOfAccountId) : IReq<byte[]>
    {
    }

    public class DownloadStatementOfAccountCommandHandler(IAccountingDbContextFactory contextFactory) : IReqHandler<DownloadStatementOfAccountCommand, byte[]>
    {
        public async Task<Response<byte[]>> Handle(DownloadStatementOfAccountCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            var entity = await context.StatementOfAccountDocument.FindAsync([request.StatementOfAccountId], cancellationToken);
            if (entity == null)
                return Response.Fail<byte[]>("Kein Dokument gefunden.");

            if (!File.Exists(entity.Documentpath))
                return Response.Fail<byte[]>("Dokument in der Datenbank vorhanden, aber auf dem Laufwerk nicht gefunden.");

            try
            {
                using var file = File.OpenRead(entity.Documentpath);
                using var mStream = new MemoryStream();
                await file.CopyToAsync(mStream, cancellationToken);
                return Response.Success(mStream.ToArray());
            }
            catch (Exception)
            {

            }
            return Response.Fail<byte[]>("Fehler beim Runterladen von Dokument.");
        }
    }
}
