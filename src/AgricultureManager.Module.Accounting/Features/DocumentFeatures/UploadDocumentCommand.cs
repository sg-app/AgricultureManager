using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Common;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

namespace AgricultureManager.Module.Accounting.Features.DocumentFeatures
{
    public record UploadDocumentCommand(AccountMouvementVm AccountMouvement, IBrowserFile File, string CustomDescription) : IReq<DocumentVm>
    {
    }
    public class UploadDocumentCommandHandler(
        ILogger<UploadDocumentCommandHandler> logger,
        IAccountingDbContextFactory contextFactory, 
        IAppDbContextFactory appContextFactory, 
        IMapper mapper) : IReqHandler<UploadDocumentCommand, DocumentVm>
    {
        public async Task<Response<DocumentVm>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            using var appContext = appContextFactory.CreateDbContext();
            var basePathKeyValue = await appContext.Parameter.FindAsync([AccountingParameterKeys.AccountingBaseFilePath], cancellationToken);
            var documentBasePathKeyValue = await appContext.Parameter.FindAsync([AccountingParameterKeys.AccountingBaseFilePath], cancellationToken);
            var documentSaveToDbKeyValue = await appContext.Parameter.FindAsync([AccountingParameterKeys.AccountingDocumentSaveToDatabase], cancellationToken);
            var basePath = basePathKeyValue?.Value ?? "share";
            var documentBasePath = documentBasePathKeyValue?.Value ?? "AccountingDocuments";

            var fileDir = Path.Combine(basePath, documentBasePath, GetFiscalYear(request.AccountMouvement.InputDate));
            if (!Directory.Exists(fileDir))
                Directory.CreateDirectory(fileDir);

            var destFileName = $"{request.AccountMouvement.InputDate.Date:yyyy-MM-dd}_{request.AccountMouvement.PartnerName}";
            if (!String.IsNullOrEmpty(request.CustomDescription))
                destFileName += $"_{request.CustomDescription}";
            destFileName += Path.GetExtension(request.File.Name);

            var filePath = Path.Combine(fileDir, destFileName);

            using var fStream = File.Create(filePath);
            if (fStream.Length < 4294967295)
            {
                var doc = new Document
                {
                    AccountMouvementId = request.AccountMouvement.Id,
                    Documentname = destFileName,
                    Documentpath = filePath
                };

                if (bool.TryParse(documentSaveToDbKeyValue?.Value, out var saveToDb))
                {
                    if (saveToDb)
                    {
                        using var mStream = new MemoryStream();
                        await request.File.OpenReadStream().CopyToAsync(mStream, cancellationToken);
                        doc.Content = mStream.ToArray();
                    }
                }

                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var entityEntry = await context.Document.AddAsync(doc, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                    await request.File.OpenReadStream(4294967295, cancellationToken).CopyToAsync(fStream, cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return Response.Success(mapper.Map<DocumentVm>(doc));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    logger.LogError(ex, "Fehler beim Speichern in der Datenbank. {message}", ex.Message);
                    return Response.Fail<DocumentVm>("Fehler beim Speichern in der Datenbank.");
                }
            }
            else
            {
                return Response.Fail<DocumentVm>("Datei ist zu groß. Maximal 4GB erlaubt.");
            }
        }
        private static string GetFiscalYear(DateTime? dateTime)
        {
            var currentDate = dateTime ?? DateTime.Now.Date;
            if (currentDate.Month >= 7)
                return $"{currentDate.Year}_{currentDate.AddYears(1).Year}";
            else
                return $"{currentDate.AddYears(-1).Year}_{currentDate.Year}";
        }
    }
}
