using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;

namespace AgricultureManager.Module.Accounting.Features.DocumentFeatures
{
    public record DownloadDocumentCommand(Guid DocumentId) : IReq<byte[]>
    {
    }

    public class DownloadDocumentCommandHandler(IAccountingDbContextFactory contextFactory) : IReqHandler<DownloadDocumentCommand, byte[]>
    {
        public async Task<Response<byte[]>> Handle(DownloadDocumentCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            var entity = await context.Document.FindAsync([request.DocumentId], cancellationToken);
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
